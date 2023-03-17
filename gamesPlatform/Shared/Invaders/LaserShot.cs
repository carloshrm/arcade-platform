using System.Numerics;

namespace cmArcade.Shared.Invaders
{
    public partial class LaserShot : IGameObject
    {
        public Vector2 pos { get; set; }
        public int healthPoints { get; set; } = 0;
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public bool hitSomething { get; set; }
        public bool fromPlayer { get; set; }
        public List<GraphicAsset>? decals { get; set; } = null;

        public LaserShot(IGameObject actor)
        {
            fromPlayer = actor is PlayerShip;
            pos = new Vector2(actor.pos.X + (actor.model.width / 2) - 2, actor.pos.Y);
            model = GameDecal.getInvaderDecal("laser");
            spriteSelect = fromPlayer ? 0 : 1;
        }

        public void hit()
        {
            model = GameDecal.getInvaderDecal("hit");
            spriteSelect = new Random().Next(0, 4);
            hitSomething = true;
        }

        public bool updatePosition((int row, int col) limits)
        {
            if (!hitSomething)
                pos += fromPlayer ? (VecDirection.Up * 10) : (VecDirection.Down * 6);
            return true;
        }
    }
}
