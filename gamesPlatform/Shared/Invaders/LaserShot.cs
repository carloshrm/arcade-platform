using System.Numerics;

namespace cmArcade.Shared.Invaders
{
    public partial class LaserShot : IGameObject
    {
        public Vector2 position { get; set; }
        public int healthPoints { get; set; } = 0;
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public bool hitSomething { get; set; }
        public bool fromPlayer { get; set; }
        public List<GraphicAsset>? decals { get; set; } = null;

        public Vector2 movingDirection { get; set; }
        public float movingSpeed { get; set; } = 6;

        public LaserShot(IGameObject actor)
        {
            fromPlayer = actor is PlayerShip;
            position = new Vector2(actor.position.X + (actor.model.width / 2) - 2, actor.position.Y);
            model = GameDecal.getInvaderDecal("laser");
            spriteSelect = actor is PlayerShip ? 0 : 1;
            movingDirection = actor is PlayerShip ? (VecDirection.Up * (movingSpeed * 2)) : (VecDirection.Down * movingSpeed);
        }

        public void hit()
        {
            model = GameDecal.getInvaderDecal("hit");
            spriteSelect = new Random().Next(0, 4);
            hitSomething = true;
        }

        public bool UpdatePosition((float row, float col) limits)
        {
            if (!hitSomething)
                position += movingDirection;
            return true;
        }
    }
}
