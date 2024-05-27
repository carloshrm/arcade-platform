namespace cmArcade.Shared.Breaker
{
    public class BreakerField : IGameField
    {
        public PlayerPad player { get; set; }
        public List<List<Block>> blocks { get; set; }
        public List<Ball> balls { get; set; }
        public List<PowerUp> powerups { get; set; }
        public (int row, int col) limits { get; }
        public int fieldScoreMultiplier { get; set; }
        public IList<string> fieldMessages { get; }
        public bool ballOnHold { get; set; }
        private int breakCount { get; set; }

        private static readonly int baseScore = 40;
        private static readonly int rowCount = 5;

        public BreakerField((float row, float col) limits)
        {
            this.limits = ((int)limits.row, (int)limits.col);
            fieldMessages = new List<string>();
            player = new PlayerPad(this.limits.row - (this.limits.row / 14), this.limits.col / 2);
            balls = new List<Ball>();
            powerups = new List<PowerUp>();
            blocks = BlockFactory.setupBlockField(this.limits, rowCount);
            fieldScoreMultiplier = 1;
            ballOnHold = true;
            breakCount = 0;
            SetBall();
        }

        public void SetBall()
        {
            balls.Add(new Ball(player.position.Y - (BallModel.breakerBall.height * 1.2f), player.position.X + (player.model.width / 2f)));
        }

        public void UpdateGameState(Score s)
        {
            player.UpdatePosition(limits);
            CheckCollisions();
            if (balls.RemoveAll(b => !b.UpdatePosition(limits)) > 0) player.loseLife();
            UpdateBallState();
            UpdatePowerups();
            CheckPowerupActivation();
            BlockCleanup(s);
        }

        public bool CheckGameOver()
        {
            foreach (var bk in blocks[0])
            {
                if (bk.position.Y + bk.model.height >= player.position.Y + player.model.height)
                    return true;
            }
            return player.healthPoints <= 0;
        }

        private void UpdatePowerups()
        {
            powerups.ForEach(p => p.UpdatePosition(limits));
        }

        private void UpdateBallState()
        {
            if (!ballOnHold)
            {
                if (balls.Count == 0)
                {
                    ballOnHold = true;
                    SetBall();
                }
            }
            else
            {
                balls.First().Follow(player.position.X + (player.model.width / 2));
            }
        }

        private void BlockCleanup(Score s)
        {
            int totalScore = 0;
            int nRowsCleared = blocks.RemoveAll(row =>
            {
                int countBefore = row.Count;
                row.RemoveAll(block =>
                    {
                        if (block.healthPoints <= 0)
                        {
                            totalScore += block.scoreMultiplier * (baseScore + s.turn) * fieldScoreMultiplier;
                            return true;
                        }
                        else
                            return false;
                    });
                breakCount += countBefore - row.Count;
                return row.Count == 0;
            });

            if (nRowsCleared > 0 || breakCount >= 14)
            {
                SpawnNewBlockRow();
                breakCount = 0;
                s.turn++;
            }

            s.scoreValue += totalScore;
        }

        private void SpawnNewBlockRow()
        {
            blocks.ForEach(r => r.ForEach(bk => bk.DropRow()));
            blocks.Add(BlockFactory.makeRandomizedRow(limits, 0));
        }

        private void ReleasePowerup(PowerUp? pu)
        {
            if (pu != null)
                powerups.Add(pu);
        }

        private void CheckPowerupActivation()
        {
            for (int i = powerups.Count - 1; i >= 0; i--)
            {
                if (CheckHit(powerups[i], player))
                {
                    powerups[i].effect?.runEffect(this);
                    powerups.RemoveAt(i);
                }
            }
        }

        private void CheckCollisions()
        {
            foreach (var ball in balls)
            {
                if (CheckHit(ball, player))
                {
                    ball.Bounce(-1, -1);
                    ball.OffsetVector(CalculateOffsetPct(ball, player.position.X, player.model.width));
                }
                else if ((ball.position.X <= 0) || (ball.position.X + ball.model.width >= limits.col))
                    ball.Bounce(1, -1);
                else if (ball.position.Y <= 0)
                    ball.Bounce(-1, 1);
                else
                {
                    foreach (var block in blocks.SelectMany(row => row.Where(b => CheckHit(b, ball))))
                    {
                        if (block.model.spriteId.Contains("fragile"))
                            block.Hit();
                        else
                        {
                            ball.Bounce(-1, 1);
                            if (!ball.breakingTimeout)
                            {
                                ball.LockoutBreaks();
                                ReleasePowerup(block.Hit());
                            }
                        }
                    }
                }
            }
        }

        private static float CalculateOffsetPct(IGameObject obj, float edgePos, float edgeWidth)
        {
            float offset = edgePos + (edgeWidth / 2) - (obj.position.X + (obj.model.width / 2));
            if (offset == 0) offset = (new Random().Next(3) - 1) * 6;
            return offset * 100 / edgeWidth;
        }

        private bool CheckHit(IGameObject obj, IGameObject target)
        {
            return (obj.position.Y + obj.model.height >= target.position.Y) && (obj.position.Y <= target.position.Y + target.model.height) &&
                    (obj.position.X + obj.model.width >= target.position.X) && (obj.position.X <= target.position.X + target.model.width);
        }

        public void ParseKeyDown(string input)
        {
            if (input.Equals("a") || input.Equals("ArrowLeft"))
                player.movingDirection = VecDirection.Left;
            if (input.Equals("d") || input.Equals("ArrowRight"))
                player.movingDirection = VecDirection.Right;
            if ((input.Equals(" ") || input.Equals("ArrowUp") || input.Equals("w")) && ballOnHold)
            {
                ballOnHold = false;
                balls.First().Shoot();
            }
        }
        public void ParseKeyUp(string input)
        {
            if (player.movingDirection == VecDirection.Left && (input.Equals("a") || input.Equals("ArrowLeft")))
                player.movingDirection = VecDirection.Zero;
            if (player.movingDirection == VecDirection.Right && (input.Equals("d") || input.Equals("ArrowRight")))
                player.movingDirection = VecDirection.Zero;
        }

        public Object GetPlayer()
        {
            return player;
        }

        public void SetScoreMultiplier(int val)
        {
            fieldScoreMultiplier += val;
        }

        public void ShowFieldMessage(string msg)
        {
            fieldMessages.Add(msg);            
            Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith((_) => fieldMessages.Remove(msg));
        }

    }
}
