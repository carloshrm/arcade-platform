namespace cmArcade.Shared.Breaker
{
    public class BreakerField
    {
        public PlayerPad player { get; set; }
        public List<List<Block>> blocks { get; set; }
        public List<Ball> balls { get; set; }
        public List<PowerUp> powerups { get; set; }
        public (int row, int col) limits { get; set; }
        private int blocksPerRow { get; init; }

        private static int baseScore = 40;
        private static int rowCount = 5;
        public bool ballOnHold = true;

        public BreakerField((int row, int col) limits)
        {
            this.limits = limits;
            player = new PlayerPad(limits.row - (limits.row / 14), limits.col / 2);
            balls = new List<Ball>();
            powerups = new List<PowerUp>();
            blocks = BlockFactory.setupBlockField(limits, rowCount);
            Console.WriteLine(blocks.Count);
            blocksPerRow = blocks.First().Count;
            setBall();
        }

        public void setBall()
        {
            balls.Add(new Ball(player.row - (limits.row / 10), player.col + (player.model.width / 2)));
        }

        public int updateFieldState()
        {
            player.updatePosition(limits);
            checkCollisions();
            if (balls.RemoveAll(b => !b.updatePosition(limits)) > 0) player.loseLife();
            updateBallState();
            updatePowerups();
            checkPowerupPickup();
            return blockCleanup();
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
            if (blocks.RemoveAll(row =>
            {
                row.RemoveAll(block =>
                    {
                        if (block.healthPoints <= 0)
                        {
                            totalScore += baseScore + (block.scoreMultiplier * baseScore);
                            return true;
                        }
                        else
                            return false;
                    });
                return row.Count == 0;
            }) > 0)
            {
                dropBlockRow();
                blocks.Add(BlockFactory.makeRandomizedRow(limits, 0));
            }
            return totalScore;
        }

        private void dropBlockRow()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                for (int j = 0; j < blocks[i].Count; j++)
                {
                    blocks[i][j].row += BlockModel.highestBlockSize;
                }
            }
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
                    powerups[i].effect?.runEffect(powerups[i].type switch
                    {
                        PowerUpType.health => player,
                        PowerUpType.ball => this,
                        _ => player,
                    });
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
                {
                    ball.bounce(1, -1);
                }
                else if (ball.row <= 0)
                {
                    ball.bounce(-1, 1);
                }
                else
                {
                    foreach (var row in blocks.Where(r => r.First().row <= ball.row + ball.model.height))
                    {
                        foreach (var block in row.Where(b => checkHit(b, ball)))
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
            if (input.Equals(" ") || input.Equals("ArrowUp") || (input.Equals("w") && ballOnHold))
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
