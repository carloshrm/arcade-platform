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

        public BreakerField((int row, int col) limits)
        {
            this.limits = limits;
            fieldMessages = new List<string>();
            player = new PlayerPad(limits.row - (limits.row / 14), limits.col / 2);
            balls = new List<Ball>();
            powerups = new List<PowerUp>();
            blocks = BlockFactory.setupBlockField(limits, rowCount);
            fieldScoreMultiplier = 1;
            ballOnHold = true;
            breakCount = 0;
            setBall();
        }

        public void setBall()
        {
            balls.Add(new Ball(player.pos.Y - (BallModel.breakerBall.height * 1.2f), player.pos.X + (player.model.width / 2f)));
        }

        public void updateGameState(Score s)
        {
            player.updatePosition(limits);
            checkCollisions();
            if (balls.RemoveAll(b => !b.updatePosition(limits)) > 0) player.loseLife();
            updateBallState();
            updatePowerups();
            checkPowerupPickup();
            blockCleanup(s);
        }

        public bool checkGameOver()
        {
            foreach (var bk in blocks[0])
            {
                if (bk.pos.Y + bk.model.height >= player.pos.Y + player.model.height)
                    return true;
            }
            return player.healthPoints <= 0;
        }

        private void updatePowerups()
        {
            powerups.ForEach(p => p.updatePosition(limits));
        }

        private void updateBallState()
        {
            if (!ballOnHold)
            {
                if (balls.Count == 0)
                {
                    ballOnHold = true;
                    setBall();
                }
            }
            else
            {
                balls.First().follow(player.pos.X + (player.model.width / 2));
            }
        }

        private void blockCleanup(Score s)
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
                newBlockRow();
                breakCount = 0;
                s.turn++;
            }

            s.scoreValue += totalScore;
        }

        private void newBlockRow()
        {
            blocks.ForEach(r => r.ForEach(bk => bk.dropRow()));
            blocks.Add(BlockFactory.makeRandomizedRow(limits, 0));
        }

        private void releasePowerup(PowerUp? pu)
        {
            if (pu != null)
                powerups.Add(pu);
        }

        private void checkPowerupPickup()
        {
            for (int i = powerups.Count - 1; i >= 0; i--)
            {
                if (checkHit(powerups[i], player))
                {
                    powerups[i].effect?.runEffect(this);
                    powerups.RemoveAt(i);
                }
            }
        }

        private void checkCollisions()
        {
            foreach (var ball in balls)
            {
                if (checkHit(ball, player))
                {
                    ball.bounce(-1, -1);
                    ball.offsetVector(calcOffsetPercentage(ball, player.pos.X, player.model.width));
                }
                else if ((ball.pos.X <= 0) || (ball.pos.X + ball.model.width >= limits.col))
                    ball.bounce(1, -1);
                else if (ball.pos.Y <= 0)
                    ball.bounce(-1, 1);
                else
                {
                    foreach (var block in blocks.SelectMany(row => row.Where(b => checkHit(b, ball))))
                    {
                        if (block.model.spriteId.Contains("fragile"))
                            block.hit();
                        else
                        {
                            ball.bounce(-1, 1);
                            if (!ball.breakingTimeout)
                            {
                                ball.lockBreak();
                                releasePowerup(block.hit());
                            }
                        }
                    }
                }
            }
        }

        private static float calcOffsetPercentage(IGameObject obj, float edgePos, float edgeWidth)
        {
            float offset = edgePos + (edgeWidth / 2) - (obj.pos.X + (obj.model.width / 2));
            if (offset == 0) offset = (new Random().Next(3) - 1) * 6;
            return offset * 100 / edgeWidth;
        }

        private bool checkHit(IGameObject obj, IGameObject target)
        {
            return (obj.pos.Y + obj.model.height >= target.pos.Y) && (obj.pos.Y <= target.pos.Y + target.model.height) &&
                    (obj.pos.X + obj.model.width >= target.pos.X) && (obj.pos.X <= target.pos.X + target.model.width);
        }

        public void parseKeyDown(string input)
        {
            if (input.Equals("a") || input.Equals("ArrowLeft"))
                player.movingDir = Direction.Left;
            if (input.Equals("d") || input.Equals("ArrowRight"))
                player.movingDir = Direction.Right;
            if ((input.Equals(" ") || input.Equals("ArrowUp") || input.Equals("w")) && ballOnHold)
            {
                ballOnHold = false;
                balls.First().shoot();
            }
        }
        public void parseKeyUp(string input)
        {
            if (player.movingDir == Direction.Left && (input.Equals("a") || input.Equals("ArrowLeft")))
                player.movingDir = Direction.Zero;
            if (player.movingDir == Direction.Right && (input.Equals("d") || input.Equals("ArrowRight")))
                player.movingDir = Direction.Zero;
        }

        public Object GetPlayer()
        {
            return player;
        }

        public void setScoreMultiplier(int val)
        {
            fieldScoreMultiplier += val;
        }

        public void setMessage(string msg)
        {
            fieldMessages.Add(msg);
            Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith((task) => fieldMessages.Remove(msg));
        }

    }
}
