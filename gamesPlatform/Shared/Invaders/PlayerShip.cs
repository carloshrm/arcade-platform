namespace gamesPlatform.Shared
{
    public class PlayerShip : GameActor
    {
        public int healthPoints { get; set; }
        public override int row { get; set; }
        public override int col { get; set; }
        public int shipWidth { get; set; }
        public Direction movingNow { get; set; }
        public Direction movingOverlap { get; set; }
        public bool canShoot { get; set; }

        public PlayerShip()
        {
            row = 550;
            col = 400;
            movingNow = Direction.none;
            movingOverlap = Direction.none;
            canShoot = true;
            healthPoints = 1;
        }

        public override void updatePosition(int rowEdge, int colEdge)
        {
            if (col >= 0 || col <= colEdge - shipWidth - 1)
                col += (int)movingNow;
        }

        public async Task shotTimeout()
        {
            canShoot = false;
            await Task.Delay(500);
            canShoot = true;
        }


    }

}
