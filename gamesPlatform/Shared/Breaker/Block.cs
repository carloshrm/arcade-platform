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
        public int scoreMultiplier { get; init; }
        public Block(int row, int col, BlockModel model)
        {
            this.model = model;
            this.row = row;
            this.col = col - (model.width / 2);
            this.healthPoints = model.HP;
            this.scoreMultiplier = model.HP / 2;
            this.spriteSelect = 0;
            decals = new List<GraphicAsset>();
        }

        public override bool updatePosition((int row, int col) limits)
        {
            return false;
        }

        public void hit()
        {
            healthPoints--;
            if (healthPoints > 0) decals.Add(GameDecal.breakerDecals["crack"]);
        }
    }
}
