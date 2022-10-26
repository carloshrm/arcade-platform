namespace cmArcade.Shared.Breaker
{
    public class Block : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override int healthPoints { get; set; }
        public override GraphicAsset model { get; set; }
        public override int spriteSelect { get; set; }
        public override List<GraphicAsset>? decals { get; set; }
        private PowerUp powerupHolder { get; set; }
        public int scoreMultiplier { get; init; }

        public Block(int row, int col, BlockModel model, int sprite)
        {
            this.model = model;
            this.row = row;
            this.col = col - (model.width / 2);
            healthPoints = model.HP;
            scoreMultiplier = model.isSpecial ? model.HP * 10 : model.HP;
            spriteSelect = sprite;
            decals = new List<GraphicAsset>();
        }

        public void setPowerup(PowerUp p)
        {
            powerupHolder = p;
        }

        public PowerUp? hit()
        {
            healthPoints--;

            if (healthPoints > 0 && !((BlockModel)model).isSpecial)
            {
                decals.Add(GameDecal.breakerDecals["crack"]);
                return null;
            }
            else
            {
                spriteSelect = spriteSelect == 0 ? 4 : spriteSelect * 2;
                return powerupHolder;
            }
        }
    }
}
