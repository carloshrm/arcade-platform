namespace cmArcade.Shared.Breaker
{
    public class BreakerBall : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override int healthPoints { get; set; }
        public override GameAsset model { get; set; }
        public override int spriteSelect { get; set; }
        private (int row, int col) movementOffset;

        public BreakerBall(int row, int col)
        {
            this.row = row;
            this.col = col;
            healthPoints = 0;
            spriteSelect = 0;
            movementOffset = (3, 0);
            model = BallModel.breakerBall;
        }

        public void bounce()
        {
            movementOffset = (-3, 0);
        }

        public override bool updatePosition((int row, int col) limits)
        {
            if (row <= limits.row && row >= 0 &&
                col <= limits.col && col >= 0)
            {
                row += movementOffset.row;
                col += movementOffset.col;
                return true;
            }
            else
                return false;
        }
    }
}
