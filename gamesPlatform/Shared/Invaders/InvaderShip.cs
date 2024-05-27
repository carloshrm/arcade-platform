using System.Numerics;

namespace cmArcade.Shared.Invaders
{
    public class InvaderShip : IGameObject
    {
        public Vector2 position { get; set; }
        public int healthPoints { get; set; }
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public List<GraphicAsset>? decals { get; set; } = null;

        public Vector2 movingDirection { get; set; } = VecDirection.Right;
        public float movingSpeed { get; set; } = 18;

        public InvaderShip(float row, float col, ShipModel model)
        {
            position = new Vector2(col, row);
            this.model = model;
            healthPoints = 1;
            spriteSelect = 0;
        }

        public void FlipDirection()
        {
            movingDirection = movingDirection == VecDirection.Left ? VecDirection.Right : VecDirection.Left;
        }

        public void Animate()
        {
            spriteSelect = spriteSelect == 0 ? 1 : 0;
        }

        public bool UpdatePosition((float row, float col) limits)
        {
            position += new Vector2((movingDirection == VecDirection.Left ? -1 : 1) * movingSpeed, 0);
            return position.X <= 0 + model.width || position.X >= limits.col - (model.width * 1.5);
        }

        public void DropRow(float rowEdge)
        {
            position += new Vector2(0, rowEdge / 20);
        }

    }
}
