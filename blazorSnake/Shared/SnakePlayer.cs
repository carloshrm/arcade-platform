namespace blazorSnake.Shared
{
    public class SnakePlayer
    {
        public class TailPiece
        {
            public (int r, int c) pos { get; set; }
            public int val { get; set; }
            public TailPiece((int r, int c) pos, int val)
            {
                this.pos = pos;
                this.val = val;
            }
        }

        public int size { get; set; }
        public (int r, int c) headPosition { get; set; }
        public List<TailPiece> tail { get; set; }
        public (int row, int col) movingDirection { get; set; }

        public SnakePlayer(int startingSize)
        {
            size = startingSize;
            tail = new List<TailPiece>();
        }

        public void setStartingValues(int r, int c)
        {
            headPosition = (r / 2, c / 2);
            movingDirection = (0, 1);
        }

        public void feedSnake()
        {
            size++;
        }

        public void setNextSnakePosition()
        {
            headPosition = (headPosition.r + movingDirection.row, headPosition.c + movingDirection.col);
        }

        public void parseMoveCommand(string keyValue)
        {
            switch (keyValue)
            {
                case "ArrowUp":
                    if (movingDirection != (1, 0))
                        movingDirection = (-1, 0);
                    break;
                case "ArrowDown":
                    if (movingDirection != (-1, 0))
                        movingDirection = (1, 0);
                    break;
                case "ArrowLeft":
                    if (movingDirection != (0, 1))
                        movingDirection = (0, -1);
                    break;
                case "ArrowRight":
                    if (movingDirection != (0, -1))
                        movingDirection = (0, 1);
                    break;
                default:
                    break;
            }
        }
    }
}
