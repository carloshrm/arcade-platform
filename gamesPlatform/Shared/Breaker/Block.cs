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
        private PowerUp? powerupHolder { get; set; }
        public int scoreMultiplier { get; init; }

        public Block(int row, int col, BlockModel model, int sprite)
        {
            this.model = model;
            this.row = row;
            this.col = col;
            healthPoints = model.HP;
            scoreMultiplier = model.isSpecial ? model.HP * 10 : model.HP;
            spriteSelect = sprite % BlockModel.variationCount;
            decals = new List<GraphicAsset>();
            powerupHolder = null;
        }

        public void setPowerup(PowerUp p)
        {
            powerupHolder = p;
        }

        public void addDecal(GameDecal d)
        {
            decals.Add(d);
        }

        public void dropRow()
        {
            row += BlockModel.highestBlockSize;
            if (powerupHolder != null)
                powerupHolder.row += BlockModel.highestBlockSize;
        }

        public PowerUp? hit()
        {
            healthPoints--;

            if (healthPoints > 0 && !((BlockModel)model).isSpecial)
            {
                addDecal(GameDecal.breakerDecals["crack"]);
                return null;
            }
            else
            {
                if (powerupHolder != null)
                {
                    spriteSelect += 5;
                    decals.Remove(decals.Find(x => x.spriteId.Contains("powerup")));
                    var p = powerupHolder;
                    powerupHolder = null;
                    return p;
                }
                else
                    return null;
            }
        }
    }
}
