namespace cmArcade.Shared
{
    public class InvaderShip : GameObject
    {
        public override int row { get; set; }
        public override int col { get; set; }
        public override int healthPoints { get; set; }
        public override GameAsset model { get; set; }
        private static Direction movingDirection = Direction.right;

        public InvaderShip(int row, int col, ShipModel model)
        {
            this.row = row;
            this.col = col;
            this.model = model;
            healthPoints = 1;
        }

        public static void flipDirection()
        {
            movingDirection = movingDirection == Direction.left ? Direction.right : Direction.left;
        }

        public override bool updatePosition(int rowEdge, int colEdge)
        {
            col += (int)movingDirection * 20;
            return col <= 0 + model.width || col >= colEdge - (model.width * 1.5);
        }

        public void dropRow(int rowEdge)
        {
            row += rowEdge / 20;
        }

    }
}
