namespace cmArcade.Shared.Breaker
{
    public class Block : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override int healthPoints { get; set; }
        public override GraphicAsset model { get; set; }
        public override int spriteSelect { get; set; }

        public Block(int row, int col)
        {
            this.model = BlockModel.block;
            this.row = row;
            this.col = col - (model.width / 2);
            this.healthPoints = 1;
            this.spriteSelect = 0;
        }

        public override bool updatePosition((int row, int col) limits)
        {
            return false;
        }

        public void hit()
        {
            healthPoints--;
            //spriteSelect
        }
    }
}
