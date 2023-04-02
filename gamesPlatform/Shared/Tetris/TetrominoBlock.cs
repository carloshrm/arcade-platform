namespace cmArcade.Shared.Tetris
{
    public class TetrominoBlock : CanvasRenderedAsset
    {
        public override string color { get; init; }
        public override float width { get; init; }
        public override float height { get; init; }

        public static readonly TetrominoBlock simpleBlock = new TetrominoBlock { color = "white", width = 1, height = 1 };
    }
}
