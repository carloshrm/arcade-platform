using System.Numerics;

namespace cmArcade.Shared.Tetris
{
    public class TetrisPlayer : IGameObject
    {
        public Vector2 position { get; set; }
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public List<GraphicAsset>? decals { get; set; }
        public int healthPoints { get; set; }

        public Vector2 movingDirection { get; set; } = VecDirection.Down;
        public float movingSpeed { get; set; } = 1;

        public TetrisPlayer()
        {
            model = TetrisPlayerModel.player;
            spriteSelect = 0;
            position = Vector2.Zero;
        }

        public bool UpdatePosition((float row, float col) limits)
        {
            position += movingDirection;
            return true;
        }
    }
}
