using System.Numerics;

namespace cmArcade.Shared.Tetris
{
    public class TetrisPlayer : IGameObject
    {
        public Vector2 pos { get; set; }
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public List<GraphicAsset>? decals { get; set; }
        public int healthPoints { get; set; }

        public TetrisPlayer()
        {
            model = TetrisPlayerModel.player;
            spriteSelect = 0;
            pos = Vector2.Zero;
        }

        public bool UpdatePosition((float row, float col) limits)
        {
            pos += VecDirection.Down;
            return true;
        }
    }
}
