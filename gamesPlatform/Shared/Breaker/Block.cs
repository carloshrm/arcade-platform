using System.Numerics;

namespace cmArcade.Shared.Breaker
{
    public class Block : IGameObject
    {
        public Vector2 pos { get; set; }
        public int healthPoints { get; set; }
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public List<GraphicAsset>? decals { get; set; }
        private PowerUp? powerupHolder { get; set; }
        public int scoreMultiplier { get; init; }

        public Block(int row, int col, BlockModel model, int sprite)
        {
            this.model = model;
            pos = new Vector2(col, row);
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
            decals?.Add(d);
        }

        public void dropRow()
        {
            pos += new Vector2(0, BlockModel.highestBlockSize);
            Console.WriteLine(pos);
            if (powerupHolder != null)
                powerupHolder.pos += new Vector2(0, BlockModel.highestBlockSize);
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
                if (powerupHolder != null && decals != null)
                {
                    spriteSelect += 5;
                    decals.Remove(decals.Find(x => x.spriteId.Contains("powerup"))!);
                    var p = powerupHolder;
                    powerupHolder = null;
                    return p;
                }
                else
                    return null;
            }
        }

        public bool updatePosition((int row, int col) limits)
        {
            throw new NotImplementedException();
        }
    }
}
