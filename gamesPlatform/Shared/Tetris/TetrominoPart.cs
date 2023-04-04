using System.Numerics;

namespace cmArcade.Shared.Tetris
{
    public class TetrominoPart : ITetrisElement
    {
        public CanvasRenderedAsset model { get; set; }
        public Vector2 pos { get; set; }
        public bool isPivot { get; set; }

        public TetrominoPart(Vector2 pos)
        {
            this.pos = pos;
            model = TetrominoBlock.simpleBlock;
            isPivot = false;
        }

        public override string? ToString()
        {
            return "pos: " + pos + " || pvt? " + isPivot;
        }
    }
}
