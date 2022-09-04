namespace gamesPlatform.Shared
{
    public class AlienShip : GameActor
    {
        public int healthPoints { get; set; }
        public override int row { get; set; }
        public override int col { get; set; }
        public int shipWidth { get; set; }
        public int shipHeight { get; set; }
        private Direction movingDirection { get; set; }

        public AlienShip(int row, int col)
        {
            this.row = row;
            this.col = col;
            healthPoints = 1;
            shipWidth = 30;
            shipHeight = 20;
            movingDirection = Direction.right;
        }

        public override void updatePosition(int rowEdge, int colEdge)
        {
            Console.WriteLine(row);
            Console.WriteLine(col);
            if (col <= 0 || col >= colEdge)
            {
                row += 6;
                movingDirection = movingDirection == Direction.left ? Direction.right : Direction.left;
            }
            col += (int)movingDirection * shipWidth;
        }

    }
}
