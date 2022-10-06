namespace cmArcade.Shared.Invaders
{
    public class InvaderShip : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override int healthPoints { get; set; }
        public override GraphicAsset model { get; set; }
        public override int spriteSelect { get; set; }
        private static Direction movingDirection = Direction.right;

        public InvaderShip(int row, int col, ShipModel model)
        {
            this.row = row;
            this.col = col;
            this.model = model;
            healthPoints = 1;
            spriteSelect = 0;
        }

        public static void flipDirection()
        {
            movingDirection = movingDirection == Direction.left ? Direction.right : Direction.left;
        }

        public void animate()
        {
            spriteSelect = spriteSelect == 0 ? 1 : 0;
        }

        public override bool updatePosition((int row, int col) limits)
        {
            col += (int)movingDirection * 18;
            return col <= 0 + model.width || col >= limits.col - (model.width * 1.5);
        }

        public void dropRow(int rowEdge)
        {
            row += rowEdge / 20;
        }

    }
}
