namespace cmArcade.Shared.Tetris
{
    public class TetrisEdgeModel : CanvasRenderedAsset
    {
        public override string color { get; init; }
        public override float width { get; init; }
        public override float height { get; init; }

        public static readonly TetrisEdgeModel simpleEdge = new TetrisEdgeModel { color = "gray", height = 1, width = 1 };
    }
}
