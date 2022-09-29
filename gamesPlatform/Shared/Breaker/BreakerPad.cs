namespace cmArcade.Shared.Breaker
{
    internal class BreakerPad : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override int healthPoints { get; set; }
        public override GameAsset model { get; set; }
        public override int spriteSelect { get; set; }
        public Direction movingDir { get; set; }
        public double accel { get; set; }


        public override bool updatePosition((int row, int col) limits)
        {
            if (col >= 0 && col <= limits.col - model.width - 1)
                col += (int)accel;
            else if (col < 0)
                col = 1;
            else
                col = limits.col - model.width - 2;

            if (movingDir == Direction.right)
            {
                accel = accel >= 6 ? 6 : accel + 0.5;
            }
            else if (movingDir == Direction.left)
            {
                accel = accel <= -6 ? -6 : accel - 0.5;
            }
            else
                accel = accel > 0 ? (accel - 0.5) : (accel + 0.5);

            return true;
        }
    }
}
