namespace cmArcade.Shared.Breaker
{
    public class BreakerGame
    {
        public BreakerPad player { get; set; }
        //list<blocks>
        public List<BreakerBall> balls { get; set; }
        public (int row, int col) limits { get; set; }

        public BreakerGame((int row, int col) limits)
        {
            this.player = new BreakerPad(limits.row - (limits.row / 14), limits.col / 2);
            this.balls = new List<BreakerBall>();
            this.limits = limits;
            setBall(limits.row / 2, player.col + (player.model.width / 2) + 10);
        }

        private void setBall(int row, int col)
        {
            balls.Add(new BreakerBall(row, col));
        }

        public void updateFieldState()
        {
            player.updatePosition(limits);
            checkCollision();
            balls.RemoveAll(b => !b.updatePosition(limits));
            if (balls.Count == 0) setBall(limits.row / 2, player.col);
        }

        private void checkCollision()
        {
            foreach (var b in balls)
            {
                if (checkHit(b, player))
                {
                    b.bounce(-1, -1);
                    b.offsetVector(calcOffsetPercentage(b, player.col, player.model.width));
                }
                else if ((b.col <= 0) || (b.col >= limits.col))
                {
                    b.bounce(1, -1);
                }
                else if (b.row <= 0)
                {
                    b.bounce(-1, 1);
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
