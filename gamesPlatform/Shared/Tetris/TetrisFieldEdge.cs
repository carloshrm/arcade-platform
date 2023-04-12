using System.Numerics;

namespace cmArcade.Shared.Tetris
{
    public class TetrisFieldEdge : ITetrisElement
    {
        public CanvasRenderedAsset model { get; set; }
        public Vector2 pos { get; set; }

        public TetrisFieldEdge(Vector2 pos)
        {
            this.pos = pos;
            model = TetrisEdgeModel.simpleEdge;
        }
    }
}
