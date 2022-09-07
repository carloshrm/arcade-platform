namespace cmArcade.Shared
{
    public class LaserShot
    {
        public const int length = 20;
        public int col { get; set; }
        public int row { get; set; }
        public bool fromPlayer { get; set; }
        public bool hit { get; set; }

        public LaserShot(GameActor actor)
        {
            this.fromPlayer = actor is PlayerShip;
            this.row = actor.row;
            this.col = actor.col;
        }

        public void updatePosition()
        {
            this.row += fromPlayer ? -6 : 4;
        }

    }
}
