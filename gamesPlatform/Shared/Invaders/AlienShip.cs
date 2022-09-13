namespace cmArcade.Shared
{
    public class AlienShip : GameActor
    {
        public override int healthPoints { get; set; }
        public override int row { get; set; }
        public override int col { get; set; }
        public override ShipModel model { get; set; }

        private static Direction movingDirection = Direction.right;
        private static int stepSize = 20;

        public AlienShip(int row, int col, ShipModel model)
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
            col += (int)movingDirection * stepSize;
            return col <= 0 || col >= colEdge - model.width;
        }

        public void dropRow(int rowEdge)
        {
            row += rowEdge / 12;
        }

    }
}
