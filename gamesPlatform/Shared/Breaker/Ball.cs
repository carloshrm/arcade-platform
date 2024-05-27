using System.Numerics;

namespace cmArcade.Shared.Breaker
{
    public class Ball : IGameObject
    {
        public Vector2 position { get; set; }
        public int healthPoints { get; set; }
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public List<GraphicAsset>? decals { get; set; }
        public Vector2 movingDirection { get; set; } = Vector2.Zero;
        public float movingSpeed { get; set; } = 0;

        public bool breakingTimeout = false;
        private bool bouncingTimeout = false;

        public Ball(float row, float col)
        {
            model = BallModel.breakerBall;
            position = new Vector2(col - (model.width / 2), row - 10);
            healthPoints = 0;
            spriteSelect = 0;
            movingDirection = new Vector2(0, 0);
        }

        public void Follow(float c)
        {
            position = new Vector2(c - (model.width / 2), position.Y);
        }

        public void Shoot()
        {
            movingDirection = new Vector2(0, -4);
        }

        public void LockoutBreaks()
        {
            if (!breakingTimeout)
            {
                breakingTimeout = true;
                Task.Delay(TimeSpan.FromMilliseconds(50)).ContinueWith((t) => breakingTimeout = false);
            }
        }

        public async void Bounce(int rDir, int cDir)
        {
            if (!bouncingTimeout)
            {
                bouncingTimeout = true;
                movingDirection = new Vector2(movingDirection.X * cDir, movingDirection.Y * rDir);
                // TODO - slow down?
                await Task.Delay(50);
                bouncingTimeout = false;
            }
        }

        public void OffsetVector(float accel)
        {
            movingDirection = new Vector2((float)Math.Floor(accel / -10), movingDirection.Y);
        }

        public bool UpdatePosition((float row, float col) limits)
        {
            if (position.Y <= limits.row)
            {
                position += movingDirection;
                return true;
            }
            else
                return false;
        }
    }
}
