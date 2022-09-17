using cmArcade.Shared.Invaders;

namespace cmArcade.Shared
{
    public partial class LaserShot : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override int healthPoints { get; set; } = 0;
        public override GameAsset model { get; set; }
        public bool hitSomething { get; set; }
        public bool fromPlayer { get; set; }

        public LaserShot(GameObject actor)
        {
            fromPlayer = actor is PlayerShip;
            row = actor.row;
            col = actor.col + (actor.model.width / 2) - 2;
            model = new LaserModel();
            model.spriteSelect = fromPlayer ? 0 : 1;
        }

        public void hit()
        {
            model = new HitEffect();
            hitSomething = true;
        }

        public override bool updatePosition(int rowEdge, int colEdge)
        {
            if (!hitSomething)
                row += fromPlayer ? -10 : 6;
            return true;
        }
    }
}
