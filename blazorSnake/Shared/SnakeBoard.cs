using Blazor.Extensions.Canvas.Canvas2D;

namespace blazorSnake.Shared
{
    public class SnakeBoard
    {
        private Canvas2DContext canvasContext { get; set; }
        private (int r, int c) scaleFactor { get; set; }
        private (int r, int c) limits { get; set; }
        private (int r, int c) foodPosition { get; set; }
        public SnakePlayer snake { get; set; }

        public SnakeBoard(int width, int height, (int r, int c) limits, Canvas2DContext c)
        {
            this.limits = limits;
            scaleFactor = (height / limits.r, width / limits.c);
            snake = new SnakePlayer(2, limits);
            canvasContext = c;
        }

        public void makeFood()
        {
            var rng = new Random();
            int row, col;
            bool invalid;

            do
            {
                row = rng.Next(1, limits.r - 2);
                col = rng.Next(1, limits.c - 2);

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
                snake.headPosition.r >= limits.r ||
                snake.headPosition.c >= limits.c)
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
            snake.tail.Add(new SnakePlayer.TailPiece(snake.headPosition, snake.size));
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

    }
}
