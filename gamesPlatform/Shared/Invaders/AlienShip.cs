namespace cmArcade.Shared
{
    public class AlienShip : GameActor
    {
        public static readonly ShipModel[] availableModels = new ShipModel[]
        {
            new ShipModel { type = 1, width = 100, height = 100 },
        };

        public override int healthPoints { get; set; }
        public override int row { get; set; }
        public override int col { get; set; }
        public override ShipModel model { get; set; }
        private Direction movingDirection { get; set; }

        public AlienShip(int row, int col)
        {
            this.row = row;
            this.col = col;
            healthPoints = 1;
            movingDirection = Direction.right;
            model = availableModels.FirstOrDefault();
        }

        public override void updatePosition(int rowEdge, int colEdge)
        {
            var nextMove = col + ((int)movingDirection * model.width);
            if (nextMove <= 0 || nextMove >= colEdge - model.width)
            {
                row += 10;
                movingDirection = movingDirection == Direction.left ? Direction.right : Direction.left;
                return;
            }
            col += (int)movingDirection * (model.width / 2);
        }

    }
}
