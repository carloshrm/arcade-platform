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

        private static int baseScore = 40;
        private static int rowCount = 5;
        private int breakCount = 0;
        public bool ballOnHold = true;

        public BreakerField((int row, int col) limits)
        {
            this.limits = limits;
            fieldMessages = new List<string>();
            player = new PlayerPad(limits.row - (limits.row / 14), limits.col / 2);
            balls = new List<Ball>();
            powerups = new List<PowerUp>();
            blocks = BlockFactory.setupBlockField(limits, rowCount);
            fieldScoreMultiplier = 1;
            setBall();
        }

        public void setBall() => balls.Add(new Ball(player.row - (int)(BallModel.breakerBall.height * 1.2), player.col + (player.model.width / 2)));

        public void updateFieldState(Score s)
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
                if (bk.row + bk.model.height >= player.row + player.model.height)
                    return true;
            }
            return player.healthPoints <= 0;
        }

        private void updatePowerups() => powerups.ForEach(p => p.updatePosition(limits));

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
                balls.First().follow(player.col + (player.model.width / 2));
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
                breakCount += (countBefore - row.Count);
                return row.Count == 0;
            });
            if (nRowsCleared > 0 || breakCount >= 14)
            {
                blocks.ForEach(r => r.ForEach(bk => bk.dropRow()));
                blocks.Add(BlockFactory.makeRandomizedRow(limits, 0));
                breakCount = 0;
                s.turn++;
            }
            s.scoreValue += totalScore;
        }

        private void releasePowerup(PowerUp pu)
        {
            if (pu != null) powerups.Add(pu);
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
                    ball.offsetVector(calcOffsetPercentage(ball, player.col, player.model.width));
                }
                else if ((ball.col <= 0) || (ball.col + ball.model.width >= limits.col))
                    ball.bounce(1, -1);
                else if (ball.row <= 0)
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

        private static int calcOffsetPercentage(GameObject obj, int edgePos, int edgeWidth)
        {
            double offset = edgePos + (edgeWidth / 2) - (obj.col + (obj.model.width / 2));
            if (offset == 0) offset = (new Random().Next(3) - 1) * 6;
            return (int)(offset * 100 / edgeWidth);
        }

        private bool checkHit(GameObject obj, GameObject target)
        {
            return (obj.row + obj.model.height >= target.row) && (obj.row <= target.row + target.model.height) &&
                    (obj.col + obj.model.width >= target.col) && (obj.col <= target.col + target.model.width);
        }

        public void parseKeyDown(string input)
        {
            if (input.Equals("a") || input.Equals("ArrowLeft"))
                player.movingDir = Direction.left;
            if (input.Equals("d") || input.Equals("ArrowRight"))
                player.movingDir = Direction.right;
            if ((input.Equals(" ") || input.Equals("ArrowUp") || input.Equals("w")) && ballOnHold)
            {
                ballOnHold = false;
                balls.First().shoot();
            }
        }
        public void parseKeyUp(string input)
        {
            if (player.movingDir == Direction.left && (input.Equals("a") || input.Equals("ArrowLeft")))
                player.movingDir = Direction.none;
            if (player.movingDir == Direction.right && (input.Equals("d") || input.Equals("ArrowRight")))
                player.movingDir = Direction.none;
        }

        public GameObject getPlayer() => player;

        public void setScoreMultiplier(int val) => fieldScoreMultiplier += val;

        public void setMessage(string msg)
        {
            fieldMessages.Add(msg);
            Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith((task) => fieldMessages.Remove(msg));
        }
    }
}
