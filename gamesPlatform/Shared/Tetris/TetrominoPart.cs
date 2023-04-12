using System.Numerics;

namespace cmArcade.Shared.Tetris
{
    public class TetrominoPart : ITetrisElement
    {
        private static int _colorControl;
        public static int colorControl
        {
            get => _colorControl;
            set
            {
                if (value >= TetrominoBlock.simpleColoredBlocks.Count)
                    _colorControl = 0;
                else
                    _colorControl = value;
            }
        }

        public CanvasRenderedAsset model { get; set; }
        public Vector2 pos { get; set; }
        public bool isPivot { get; set; }

        public TetrominoPart(Vector2 pos)
        {
            this.pos = pos;
            model = TetrominoBlock.simpleColoredBlocks.ElementAt(colorControl);
            isPivot = false;
        }

        public override string? ToString()
        {
            return "pos: " + pos + " || pvt? " + isPivot;
        }
    }
}
