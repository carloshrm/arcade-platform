namespace cmArcade.Shared.Tetris
{
    public class TetrisPlayerModel : GraphicAsset
    {
        public override string spriteId { get; set; }
        public override int width { get; init; }
        public override int height { get; init; }

        public static readonly TetrisPlayerModel player = new TetrisPlayerModel { width = 700, height = 700, spriteId = "tetrisBox" };
    }
}
