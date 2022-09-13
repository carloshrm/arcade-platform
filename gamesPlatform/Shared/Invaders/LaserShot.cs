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
            fromPlayer = actor is PlayerShip;
            row = actor.row;
            col = actor.col + (actor.model.width / 2) - 2;
        }

        public void updatePosition()
        {
            row += fromPlayer ? -8 : 4;
        }

    }
}
