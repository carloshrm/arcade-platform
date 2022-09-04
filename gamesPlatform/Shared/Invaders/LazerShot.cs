namespace gamesPlatform.Shared
{
    public class LaserShot : GameActor
    {
        public const int length = 3;
        public override int col { get; set; }
        public override int row { get; set; }
        private bool fromPlayer { get; set; }

        public LaserShot(GameActor actor)
        {
            this.fromPlayer = actor is PlayerShip;
            this.row = actor.row;
            this.col = actor.col;
        }

        public override void updatePosition(int rowEdge = 0, int colEdge = 0)
        {
            this.row += fromPlayer ? -2 : 1;
        }
    }
}
