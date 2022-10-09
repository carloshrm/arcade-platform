namespace cmArcade.Shared.Breaker
{
    public class Ball : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override int healthPoints { get; set; }
        public override GraphicAsset model { get; set; }
        public override int spriteSelect { get; set; }
        private (int row, int col) movementVector;
        public bool breakingTimeout = false;

        public Ball(int row, int col)
        {
            model = BallModel.breakerBall;
            this.row = row;
            this.col = col - (model.width / 2);
            healthPoints = 0;
            spriteSelect = 0;
            movementVector = (0, 0);
        }

        public void follow(int c)
        {
            this.col = c - (model.width / 2);
        }

        public void shoot()
        {
            movementVector = (4, 0);
        }

        private void lockBreak()
        {
            if (breakingTimeout) return;
            else
            {
                breakingTimeout = true;
                Task.Delay(350);
                breakingTimeout = false;
            }
        }

        public void bounce(int rDir, int cDir)
        {
            movementVector = (movementVector.row * rDir, movementVector.col * cDir);
            if (Math.Abs(movementVector.col) >= 6)
                movementVector.col = (int)(movementVector.col * 0.8);
            lockBreak();
        }

        public void offsetVector(int accel)
        {
            movementVector.col = accel / -10;
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
