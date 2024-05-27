using System.Numerics;

namespace cmArcade.Shared.Breaker
{
    public class PlayerPad : IGameObject
    {
        public Vector2 position { get; set; }
        public int healthPoints { get; set; }
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public Vector2 movingDirection { get; set; }
        public float movingSpeed { get; set; }
        public float weight { get; set; }
        public List<GraphicAsset>? decals { get; set; } = null;

        public PlayerPad(float row, float col)
        {
            position = new Vector2(col, row);
            model = PadModel.playerPad;
            movingDirection = VecDirection.Zero;
            healthPoints = 3;
            movingSpeed = 0;
            weight = 0.6f;
            spriteSelect = 0;
        }

        public void SetWeight(float w)
        {
            if (w > 0)
                weight = w;
            else
                throw new ArgumentException("weight should be positive");
        }

        public bool loseLife()
        {
            return --healthPoints <= 0;
        }

        public bool UpdatePosition((float row, float col) limits)
        {
            if (position.X >= 0 && position.X <= limits.col - model.width - 1)
            {
                position = new Vector2(position.X + movingSpeed, position.Y);
            }
            else if (position.X < 0)
            {
                position = new Vector2(1, position.Y);
            }
            else
            {
                position = new Vector2(limits.col - model.width - 2, position.Y);
            }

            if (movingDirection == VecDirection.Right)
            {
                if (movingSpeed < 0)
                    movingSpeed = 0;
                movingSpeed = movingSpeed < 8 ? movingSpeed + weight : 8;
            }
            else if (movingDirection == VecDirection.Left)
            {
                if (movingSpeed > 0)
                    movingSpeed = 0;
                movingSpeed = movingSpeed > -8 ? movingSpeed - weight : -8;
            }
            else
            {
                movingSpeed += movingSpeed > 0 ? -weight : weight;
                if (Math.Abs(movingSpeed) <= weight)
                    movingSpeed = 0;
            }
            return true;
        }
    }
}
