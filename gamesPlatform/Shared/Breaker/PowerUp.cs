namespace cmArcade.Shared.Breaker
{
    public class PowerUp : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override GraphicAsset model { get; set; }
        public override int spriteSelect { get; set; }
        public Action<GameObject>? effect { get; set; }
        public PowerupType type { get; set; }

        public PowerUp(int row, int col, PowerupType type, int sprite)
        {
            this.row = row;
            this.col = col;
            this.type = type;
            model = PowerUpModel.powerUps[type];
            spriteSelect = sprite;
            setEffect(type);
        }

        private void setEffect(PowerupType t)
        {
            switch (t)
            {
                case PowerupType.health:
                    effect = new Action<GameObject>((GameObject t) => t.healthPoints++);
                    break;
                default:
                    effect = null;
                    break;
            }
        }

        public override bool updatePosition((int row, int col) limits)
        {
            row += 1;
            return true;
        }

    }
}
