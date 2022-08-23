namespace gamesPlatform.Shared.Snake
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

        public SnakePlayer(int startingSize, (int r, int c) boardLimits)
        {
            size = startingSize;
            tail = new List<TailPiece>();
            headPosition = (boardLimits.r / 2, boardLimits.c / 2);
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
                case "w":
                    if (movingDirection != (1, 0))
                        movingDirection = (-1, 0);
                    break;
                case "ArrowDown":
                case "s":
                    if (movingDirection != (-1, 0))
                        movingDirection = (1, 0);
                    break;
                case "ArrowLeft":
                case "a":
                    if (movingDirection != (0, 1))
                        movingDirection = (0, -1);
                    break;
                case "ArrowRight":
                case "d":
                    if (movingDirection != (0, -1))
                        movingDirection = (0, 1);
                    break;
                default:
                    break;
            }
        }
    }
}
