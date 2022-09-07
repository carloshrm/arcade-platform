namespace cmArcade.Shared
{
    public class PlayerShip : GameActor
    {
        public override int healthPoints { get; set; }
        public override int row { get; set; }
        public override int col { get; set; }
        public override ShipModel model { get; set; }
        public Direction movingDir { get; set; }
        public double accel { get; set; }
        public bool canShoot { get; set; }

        public PlayerShip(int row, int col)
        {
            this.row = row;
            this.col = col;
            model = new ShipModel() { type = 0, width = 20, height = 20 };
            movingDir = Direction.none;
            canShoot = true;
            healthPoints = 1;
            accel = 0;
        }

        public override void updatePosition(int rowEdge, int colEdge)
        {

            if ((col >= 0 && movingDir == Direction.left) || (col <= colEdge - model.width - 1 && movingDir == Direction.right))
                col += (int)accel;

            if (movingDir == Direction.right)
                accel = 4;
            else if (movingDir == Direction.left)
                accel = -4;

            if (movingDir == Direction.none)
                accel = accel > 0 ? (accel - 0.1) : (accel + 0.1);
        }

        public async Task shotTimeout()
        {
            canShoot = false;
            await Task.Delay(500);
            canShoot = true;
        }


    }

}
