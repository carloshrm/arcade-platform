namespace cmArcade.Shared
{
    public class AlienShip : GameActor
    {
        public override int healthPoints { get; set; }
        public override int row { get; set; }
        public override int col { get; set; }
        public override ShipModel model { get; set; }

        private static Direction movingDirection = Direction.right;

        public AlienShip(int row, int col, int modelID)
        {
            this.row = row;
            this.col = col;
            healthPoints = 1;
            model = ShipModel.availableModels[modelID];
        }

        public static void flipDirection()
        {
            movingDirection = movingDirection == Direction.left ? Direction.right : Direction.left;
        }

        public override bool updatePosition(int rowEdge, int colEdge)
        {
            col += (int)movingDirection * (model.width / 2);
            return col <= 0 || col >= colEdge - model.width;
        }

        public void dropRow(int rowEdge)
        {
            row += rowEdge / 12;
        }

    }
}
