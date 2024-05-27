using System.Numerics;

namespace cmArcade.Shared.Breaker
{
    public class Block : IGameObject
    {
        public Vector2 position { get; set; }
        public int healthPoints { get; set; }
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public List<GraphicAsset>? decals { get; set; }
        private PowerUp? powerupHolder { get; set; }
        public int scoreMultiplier { get; init; }
        public Vector2 movingDirection { get; set; } = Vector2.Zero;
        public float movingSpeed { get; set; } = 0;

        public Block(int row, int col, BlockModel model, int sprite)
        {
            this.model = model;
            position = new Vector2(col, row);
            healthPoints = model.HP;
            scoreMultiplier = model.isSpecial ? model.HP * 10 : model.HP;
            spriteSelect = sprite % BlockModel.variationCount;
            decals = new List<GraphicAsset>();
            powerupHolder = null;
        }

        public void InsertPowerup(PowerUp p)
        {
            powerupHolder = p;
        }

        public void AddDecal(GameDecal d)
        {
            decals?.Add(d);
        }

        public void DropRow()
        {
            position += new Vector2(0, BlockModel.highestBlockSize);
            if (powerupHolder != null)
                powerupHolder.position += new Vector2(0, BlockModel.highestBlockSize);
        }

        public PowerUp? Hit()
        {
            healthPoints--;

            if (healthPoints > 0 && !((BlockModel)model).isSpecial)
            {
                AddDecal(GameDecal.breakerDecals.First(d => d.spriteId.Contains("crack")));
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

        public bool UpdatePosition((float row, float col) limits)
        {
            throw new NotImplementedException();
        }
    }
}
