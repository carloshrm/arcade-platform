using Blazor.Extensions.Canvas.Canvas2D;

namespace blazorSnake.Shared
{
    public class Board
    {
        private Canvas2DContext canvasContext { get; set; }
        private (int r, int c) scaleFactor { get; set; }
        private (int row, int col) limits { get; set; }
        private (int r, int c) foodPosition { get; set; }
        public Snake snake { get; set; }

        public Board(int x, int y)
        {
            scaleFactor = (20, 20);
            int roundedX = (int)Math.Floor(x / 10.0 * 10);
            int roundedY = (int)Math.Floor(y / 10.0 * 10);
            roundedX = roundedX < 400 ? 400 : roundedX;
            roundedY = roundedY < 400 ? 400 : roundedY;
            limits = (roundedY / scaleFactor.r, roundedX / scaleFactor.c);
        }

        public void setContext(Canvas2DContext c)
        {
            canvasContext = c;
        }

        public void makeFood()
        {
            Random rng = new Random();
            int row, col;
            bool invalid;

            do
            {
                row = rng.Next(1, limits.row - 2);
                col = rng.Next(1, limits.col - 2);

                foreach (var pc in snake.tail)
                {
                    invalid = pc.pos.c == col || pc.pos.r == row;
                }
                invalid = snake.headPosition.c == col || snake.headPosition.r == row;
            } while (invalid);

            foodPosition = (row, col);
        }

        public bool checkSpot(Action increaseSpeed)
        {
            if (snake.headPosition.r < 0 ||
                snake.headPosition.c < 0 ||
                snake.headPosition.r >= limits.row ||
                snake.headPosition.c >= limits.col)
            {
                Console.WriteLine("\n Game Over - edge.");
                return false;
            }
            else
            {
                foreach (var tp in snake.tail)
                {
                    if (tp.pos == snake.headPosition)
                    {
                        Console.WriteLine("\n Game Over - tail.");

                        return false;
                    }
                }
                if (snake.headPosition == foodPosition)
                {
                    makeFood();
                    snake.feedSnake();
                    increaseSpeed();
                }
            }
            snake.tail.Add(new Snake.TailPiece(snake.headPosition, snake.size));
            return true;
        }

        private async Task clearTail()
        {
            foreach (var tailPiece in snake.tail)
            {
                if (tailPiece.val <= 0)
                {
                    await canvasContext.ClearRectAsync(
                        tailPiece.pos.c * scaleFactor.c,
                        tailPiece.pos.r * scaleFactor.r,
                        scaleFactor.c,
                        scaleFactor.r);
                }
            }
            snake.tail.RemoveAll(tp => tp.val == 0);
        }

        private async Task drawPiece(int c, int r)
        {
            await canvasContext.FillRectAsync(c, r, scaleFactor.c, scaleFactor.r);
        }

        public async Task drawGameState()
        {
            await canvasContext.BeginBatchAsync();

            await canvasContext.SetFillStyleAsync("red");
            await drawPiece(foodPosition.c * scaleFactor.c, foodPosition.r * scaleFactor.r);
            await clearTail();

            await canvasContext.SetFillStyleAsync("green");
            await drawPiece(snake.headPosition.c * scaleFactor.c, snake.headPosition.r * scaleFactor.r);

            await canvasContext.SetFillStyleAsync("darkgreen");
            for (int i = 0; i < snake.tail.Count; i++)
            {
                await drawPiece(snake.tail[i].pos.c * scaleFactor.c, snake.tail[i].pos.r * scaleFactor.r);
                snake.tail[i].val--;
            }
            await canvasContext.EndBatchAsync();
        }

        public void newPlayer(Snake p)
        {
            snake = p;
            snake.setStartingValues(limits.row, limits.col);
        }

    }
}
