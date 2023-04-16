using System.Numerics;

namespace cmArcade.Shared.Invaders
{
    public class InvaderShip : IGameObject
    {
        public Vector2 pos { get; set; }
        public int healthPoints { get; set; }
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public List<GraphicAsset>? decals { get; set; } = null;

        private static Direction movingDirection = Direction.Right;

        public InvaderShip(int row, int col, ShipModel model)
        {
            pos = new Vector2(col, row);
            this.model = model;
            healthPoints = 1;
            spriteSelect = 0;
        }

        public static void flipDirection()
        {
            movingDirection = movingDirection == Direction.Left ? Direction.Right : Direction.Left;
        }

        public void animate()
        {
            spriteSelect = spriteSelect == 0 ? 1 : 0;
        }

        public bool updatePosition((int row, int col) limits)
        {
            pos += new Vector2((int)movingDirection * 18, 0);
            return pos.X <= 0 + model.width || pos.X >= limits.col - (model.width * 1.5);
        }

        public void dropRow(int rowEdge)
        {
            pos += new Vector2(0, rowEdge / 20);
        }

    }
}
