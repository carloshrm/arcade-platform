using System.Numerics;

namespace cmArcade.Shared
{
    public class TailPiece : ISimpleGameObject
    {
        internal class TailModel : CanvasRenderedAsset
        {
            public override string color { get; init; }
            public override float width { get; init; }
            public override float height { get; init; }

            public static readonly TailModel tailModel = new TailModel { color = "green", width = 1, height = 1 };
        }

        public Vector2 pos { get; set; }
        public int healthVal { get; set; }
        public CanvasRenderedAsset model { get; set; } = TailModel.tailModel;

        public TailPiece((int r, int c) pos, int healthVal)
        {
            this.pos = new Vector2(pos.c, pos.r);
            this.healthVal = healthVal;
        }

        public TailPiece(Vector2 pos, int healthVal)
        {
            this.pos = pos;
            this.healthVal = healthVal;
        }
    }
}
