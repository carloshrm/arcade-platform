namespace cmArcade.Shared.Invaders
{
    public class PlayerShip : GameObject
    {
        public override int healthPoints { get; set; }
        public override int row { get; set; }
        public override int col { get; set; }
        public override GameAsset model { get; set; }
        public override int spriteSelect { get; set; }
        public Direction movingDir { get; set; }
        public double accel { get; set; }

        public bool canShoot { get; set; }

        public PlayerShip(int row, int col)
        {
            this.row = row;
            this.col = col;
            model = ShipModel.playerShip;
            movingDir = Direction.none;
            canShoot = true;
            healthPoints = 3;
            accel = 0;
            spriteSelect = 0;
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
                accel = 6;
            }
            else if (movingDir == Direction.left)
            {
                accel = -6;
            }
            else
                accel = accel > 0 ? (accel - 0.5) : (accel + 0.5);

            return true;
        }

        public async Task shotTimeout()
        {
            canShoot = false;
            spriteSelect = 1;
            await Task.Delay(250);
            spriteSelect = 0;
            await Task.Delay(250);
            canShoot = true;
        }
    }

}
