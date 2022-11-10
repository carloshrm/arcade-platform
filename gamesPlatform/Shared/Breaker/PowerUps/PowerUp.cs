namespace cmArcade.Shared.Breaker
{
    public class PowerUp : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override int spriteSelect { get; set; }
        public override GraphicAsset model { get; set; }
        public PowerUpType type { get; set; }
        public IPowerUpEffect effect { get; set; }

        public PowerUp(int row, int col, PowerUpType type, int sprite)
        {
            this.type = type;
            model = PowerUpModel.powerUps[type];
            this.row = row;
            this.col = col - (model.width / 2);
            spriteSelect = sprite;
            effect = IPowerUpEffect.getPowerUp(type);
        }

        public override bool updatePosition((int row, int col) limits)
        {
            row += 2;
            return true;
        }
    }
}
