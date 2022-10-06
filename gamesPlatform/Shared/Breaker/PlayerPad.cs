namespace cmArcade.Shared.Breaker
{
    public class PlayerPad : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override int healthPoints { get; set; }
        public override GraphicAsset model { get; set; }
        public override int spriteSelect { get; set; }
        public Direction movingDir { get; set; }
        public double accel { get; set; }
        public double weight { get; set; }

        public PlayerPad(int row, int col)
        {
            this.row = row;
            this.col = col;
            model = PadModel.playerPad;
            movingDir = Direction.none;
            healthPoints = 3;
            accel = 0;
            weight = 0.5;
            spriteSelect = 0;
        }

        public void setWeight(double w)
        {
            if (w > 0)
                weight = w;
            else
                throw new ArgumentException("weight should be positive");
        }

        public bool loseLife()
        {
            healthPoints--;
            //cracked sprite
            return healthPoints <= 0;
        }

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
                accel = accel >= 6 ? 6 : accel + weight;
            }
            else if (movingDir == Direction.left)
            {
                accel = accel <= -6 ? -6 : accel - weight;
            }
            else
                accel = accel > 0 ? (accel - weight) : (accel + weight);

            return true;
        }
    }
}
