using System.Numerics;

namespace cmArcade.Shared.Invaders
{
    public class PlayerShip : IGameObject
    {
        public Vector2 position { get; set; }
        public int healthPoints { get; set; }
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public Vector2 movingDirection { get; set; }
        public float movingSpeed { get; set; }

        public bool canShoot { get; set; }
        public List<GraphicAsset>? decals { get; set; } = null;

        public PlayerShip(float row, float col)
        {
            position = new Vector2(col, row);
            model = ShipModel.playerShip;
            movingDirection = Vector2.Zero;
            canShoot = true;
            healthPoints = 3;
            movingSpeed = 0;
            spriteSelect = 0;
        }

        public bool UpdatePosition((float row, float col) limits)
        {
            if (position.X >= 0 && position.X <= limits.col - model.width - 1)
                position += new Vector2(movingSpeed, 0);
            else if (position.X < 0)
                position = new Vector2(1, position.Y);
            else
                position = new Vector2(limits.col - model.width - 2, position.Y);

            if (movingDirection == VecDirection.Right)
                movingSpeed = 6;
            else if (movingDirection == VecDirection.Left)
                movingSpeed = -6;
            else
            {
                movingSpeed += movingSpeed > 0 ? -0.5f : 0.5f;
                if (Math.Abs(movingSpeed) >= movingSpeed)
                    movingSpeed = 0;
            }

            return true;
        }

        public async Task shotTimeout()
        {
            canShoot = false;
            spriteSelect = 1;
            await Task.Delay(250);
            spriteSelect = 0;
            await Task.Delay(250);
            canShoot = true;
        }
    }

}
