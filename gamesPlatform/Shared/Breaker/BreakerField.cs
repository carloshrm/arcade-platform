namespace cmArcade.Shared.Breaker
{
    public class BreakerField
    {
        private static Random rng = new Random();
        public PlayerPad player { get; set; }
        public List<Block> blocks { get; set; }
        public List<Ball> balls { get; set; }
        public List<PowerUp> powerups { get; set; }
        public (int row, int col) limits { get; set; }

        private int baseScore = 40;
        public bool ballOnHold;
        private static int blkRows = 5;

        // TODO
        // procedurally generate a new row of blocks if game almost over and
        // drop the current row by X
        // add more block variety
        // power ups - extra balls inside blocks, stronger ball, more lives, light gravity
        // power down - heavy pad, fast ball

        public BreakerField((int row, int col) limits)
        {
            this.limits = limits;
            player = new PlayerPad(limits.row - (limits.row / 14), limits.col / 2);
            balls = new List<Ball>();
            blocks = new List<Block>();
            powerups = new List<PowerUp>();
            ballOnHold = true;
            setBall();
            blocks = BlockFactory.setupBlockField(limits, blkRows);
        }


        private void setBall()
        {
            balls.Add(new Ball(player.row - (limits.row / 10), player.col + (player.model.width / 2)));
        }

        public int updateFieldState()
        {
            player.updatePosition(limits);
            checkCollision();
            if (balls.RemoveAll(b => !b.updatePosition(limits)) > 0)
            {
                player.loseLife();
            }
            updateBallState();
            updatePowerups();
            checkPowerupPickup();
            var blockHitCount = blockCleanup();
            return blockHitCount;
        }

        public bool checkGameOver()
        {
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
                balls.First().follow(player.col + (player.model.width / 2));
            }
        }

        private int blockCleanup()
        {
            int totalScore = 0;
            blocks.RemoveAll(b =>
            {
                if (b.healthPoints <= 0)
                {
                    totalScore += baseScore + (b.scoreMultiplier * baseScore);
                    return true;
                }
                else
                    return false;
            });
            return totalScore;
        }

        private void releasePowerup(PowerUp pu)
        {
            powerups.Add(pu);
        }

        private void checkPowerupPickup()
        {
            for (int i = powerups.Count - 1; i >= 0; i--)
            {
                if (checkHit(powerups[i], player))
                {
                    powerups[i].effect?.Invoke(powerups[i].type switch
                    {
                        PowerupType.health => player,
                        _ => player,
                    });
                    powerups.RemoveAt(i);
                }
            }
        }

        private void checkCollision()
        {
            foreach (var ball in balls)
            {
                if (checkHit(ball, player))
                {
                    ball.bounce(-1, -1);
                    ball.offsetVector(calcOffsetPercentage(ball, player.col, player.model.width));
                }
                else if ((ball.col <= 0) || (ball.col + ball.model.width >= limits.col))
                {
                    ball.bounce(1, -1);
                }
                else if (ball.row <= 0)
                {
                    ball.bounce(-1, 1);
                }
                else
                {
                    foreach (var blk in blocks)
                    {
                        if (checkHit(ball, blk))
                        {
                            if (blk.model.spriteId.Contains("fragile"))
                            {
                                blk.hit();
                            }
                            else
                            {
                                ball.bounce(-1, 1);
                                if (!ball.breakingTimeout)
                                {
                                    ball.lockBreak();
                                    var powerup = blk.hit();
                                    if (powerup != null)
                                        releasePowerup(powerup);
                                }
                            }
                        }
                    }
                }
            }
            int calcOffsetPercentage(GameObject obj, int edgePos, int edgeWidth)
            {
                double offset = edgePos + (edgeWidth / 2) - (obj.col + (obj.model.width / 2));
                return (int)(offset * 100 / edgeWidth);
            }
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
            if ((input.Equals(" ") || input.Equals("ArrowUp")) && ballOnHold)
            {
                ballOnHold = false;
                balls.First().shoot();
            }
        }

        public void parseKeyUp(string input)
        {
            player.movingDir = Direction.none;
        }

    }
}
