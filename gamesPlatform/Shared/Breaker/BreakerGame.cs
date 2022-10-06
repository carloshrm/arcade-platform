namespace cmArcade.Shared.Breaker
{
    public class BreakerGame
    {
        public PlayerPad player { get; set; }
        public List<Block> blocks { get; set; }
        public List<Ball> balls { get; set; }
        public (int row, int col) limits { get; set; }

        private static int blkPerRow = 10;
        private static int blkRows = 3;
        private int blkRowPos;

        public BreakerGame((int row, int col) limits)
        {
            this.limits = limits;
            player = new PlayerPad(limits.row - (limits.row / 14), limits.col / 2);
            balls = new List<Ball>();
            blocks = new List<Block>();
            blkRowPos = limits.row / 8;
            setBall(limits.row / 2, limits.col / 2);
            setBlocks();
        }

        private void setBlocks()
        {
            int colOffset = limits.col / blkPerRow;
            int padding = colOffset - BlockModel.block.width;
            for (int i = 1; i <= blkRows; i++)
            {
                for (int j = 0; j < blkPerRow; j++)
                {
                    blocks.Add(new Block(blkRowPos + ((int)(BlockModel.block.height * 1.2) * i),
                        (colOffset * j) + (BlockModel.block.width / 2) + (padding / 2)));
                }
            }
        }

        private void setBall(int row, int col)
        {
            balls.Add(new Ball(row, col));
        }

        public void updateFieldState()
        {
            player.updatePosition(limits);
            checkCollision();
            balls.RemoveAll(b => !b.updatePosition(limits));
            blocks.RemoveAll(bk => bk.healthPoints <= 0);
            if (balls.Count == 0) setBall(limits.row / 2, player.col);
        }

        private void checkCollision()
        {
            foreach (var ball in balls)
            {
                // TODO - fix multiple collisions bug
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
                            ball.bounce(-1, 1);
                            blk.hit();
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
        }

        public void parseKeyUp(string input)
        {
            player.movingDir = Direction.none;
        }

    }
}
