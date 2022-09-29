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
            if ((col >= 0 && movingDir == Direction.left) || (col <= limits.col - model.width - 1 && movingDir == Direction.right))
                col += (int)accel;

            if (movingDir == Direction.right)
                accel = 6;
            else if (movingDir == Direction.left)
                accel = -6;

            if (movingDir == Direction.none)
                accel = accel > 0 ? (accel - 0.01) : (accel + 0.01);

            return true;
        }
    }
}
