namespace cmArcade.Shared.Breaker
{
    public class BreakerBall : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override int healthPoints { get; set; }
        public override GameAsset model { get; set; }
        public override int spriteSelect { get; set; }
        private (int row, int col) movementVector;

        public BreakerBall(int row, int col)
        {
            model = BallModel.breakerBall;
            this.row = row;
            this.col = col;
            healthPoints = 0;
            spriteSelect = 0;
            movementVector = (4, 0);
        }

        public void bounce(int rDir, int cDir)
        {
            movementVector = (movementVector.row * rDir, movementVector.col * cDir);
            if (Math.Abs(movementVector.col) >= 6)
                movementVector.col = (int)(movementVector.col * 0.8);
        }

        public void offsetVector(int accel)
        {
            movementVector.col -= accel / 10;
            Console.WriteLine(movementVector);
        }

        public override bool updatePosition((int row, int col) limits)
        {
            if (row <= limits.row)
            {
                row += movementVector.row;
                col += movementVector.col;
                return true;
            }
            else
                return false;
        }
    }
}
