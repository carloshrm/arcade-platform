namespace cmArcade.Shared.Breaker
{
    public class BreakerField
    {
        public BreakerPad player { get; set; }
        //list<blocks>
        public List<BreakerBall> balls { get; set; }
        public (int row, int col) limits { get; set; }

        public BreakerField((int row, int col) limits)
        {
            this.player = new BreakerPad(limits.row - (limits.row / 14), limits.col / 2);
            this.balls = new List<BreakerBall>();
            this.limits = limits;
            setBall(limits.row / 2, player.col);
        }

        private void setBall(int row, int col)
        {
            balls.Add(new BreakerBall(row, col));
        }

        public void updateFieldState()
        {
            player.updatePosition(limits);
            checkCollision();
            balls.RemoveAll(b => b.updatePosition(limits) == false);
            if (balls.Count == 0) setBall(limits.row / 2, player.col);
        }

        private void checkCollision()
        {
            foreach (var b in balls)
            {
                if ((b.row + b.model.height >= player.row) &&
                    b.col + b.model.width >= player.col &&
                    b.col <= player.col + player.model.width
                    )
                {
                    b.bounce();
                    Console.WriteLine("bounce at" + Math.Abs((player.model.width / 2) - (b.col + (b.model.width / 2) - player.col)));
                }
            }
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
