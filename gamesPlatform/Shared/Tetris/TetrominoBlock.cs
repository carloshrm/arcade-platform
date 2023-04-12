namespace cmArcade.Shared.Tetris
{
    public class TetrominoBlock : CanvasRenderedAsset
    {
        public override string color { get; init; }
        public override float width { get; init; }
        public override float height { get; init; }

        public static readonly IReadOnlyCollection<TetrominoBlock> simpleColoredBlocks = new List<TetrominoBlock>() {
            new TetrominoBlock { color = "yellow", width = 1, height = 1 },
            new TetrominoBlock { color = "red", width = 1, height = 1 },
            new TetrominoBlock { color = "blue", width = 1, height = 1 },
            new TetrominoBlock { color = "cyan", width = 1, height = 1 },
        };

    }
}
