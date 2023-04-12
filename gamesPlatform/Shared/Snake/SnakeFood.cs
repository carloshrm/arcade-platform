using System.Numerics;

namespace cmArcade.Shared
{
    public class SnakeFood : ISimpleGameObject
    {
        internal class SnakeFoodModel : CanvasRenderedAsset
        {
            public override string color { get; init; }
            public override float width { get; init; }
            public override float height { get; init; }

            public static readonly SnakeFoodModel snakeFoodModel = new SnakeFoodModel { color = "red", width = 1, height = 1 };
        }

        public CanvasRenderedAsset model { get; set; }
        public Vector2 pos { get; set; }

        public SnakeFood(Vector2 pos)
        {
            this.model = SnakeFoodModel.snakeFoodModel;
            this.pos = pos;
        }
    }
}
