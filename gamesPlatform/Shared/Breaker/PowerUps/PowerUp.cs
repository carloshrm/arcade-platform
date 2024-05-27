using System.Numerics;

namespace cmArcade.Shared.Breaker
{
    public class PowerUp : IGameObject
    {
        public Vector2 position { get; set; }
        public int spriteSelect { get; set; }
        public GraphicAsset model { get; set; }
        public BreakerPowerUpType type { get; set; }
        public IPowerUpEffect effect { get; set; }
        public List<GraphicAsset>? decals { get; set; } = null;
        public int healthPoints { get; set; } = 0;
        public Vector2 movingDirection { get; set; } = VecDirection.Down;
        public float movingSpeed { get; set; } = 2;

        public PowerUp(int row, int col, BreakerPowerUpType type, int sprite)
        {
            this.type = type;
            model = PowerUpModel.breakerPowerUps[type];
            position = new Vector2(col - (model.width / 2), row);
            spriteSelect = sprite;
            effect = IPowerUpEffect.getBreakerPowerUp(type);
        }

        public bool UpdatePosition((float row, float col) limits)
        {
            position += movingDirection * movingSpeed;
            return true;
        }
    }
}
