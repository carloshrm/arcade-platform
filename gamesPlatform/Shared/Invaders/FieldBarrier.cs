namespace cmArcade.Shared.Invaders
{
    public class FieldBarrier : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override int healthPoints { get; set; }
        public override GraphicAsset model { get; set; }
        public override int spriteSelect { get; set; }

        public FieldBarrier(int row, int col)
        {
            model = GameDecal.getInvaderDecal("barrier");
            this.row = row;
            this.col = col - (model.width / 2);
            healthPoints = 6;
            spriteSelect = 0;
        }

        public void hit()
        {
            healthPoints--;
            spriteSelect = healthPoints > 3 ? 0 : healthPoints == 0 ? 2 : 1;

        }

        public override bool updatePosition((int row, int col) limits)
        {
            return false;
        }
    }
}
