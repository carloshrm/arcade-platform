using Blazor.Extensions.Canvas.Canvas2D;

namespace gamesPlatform.Shared.Snake
{
    public class SnakeBoard
    {
        private Canvas2DContext canvasContext { get; set; }
        private (int r, int c) scaleFactor { get; set; }
        private (int r, int c) limits { get; set; }
        private (int r, int c) foodPosition { get; set; }
        public SnakePlayer snake { get; set; }

        public SnakeBoard((int width, int height) dimensions, (int r, int c) limits, Canvas2DContext c)
        {
            this.limits = limits;
            scaleFactor = (dimensions.height / limits.r, dimensions.width / limits.c);
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

        public bool checkCurrentSpotContents(Action increaseSpeed)
        {
            if (snake.headPosition.r < 0 ||
                snake.headPosition.c < 0 ||
                snake.headPosition.r >= limits.r ||
                snake.headPosition.c >= limits.c)
            {
                //Game Over - Edge
                return false;
            }
            else
            {
                foreach (var tp in snake.tail)
                {
                    if (tp.pos == snake.headPosition)
                    {
                        //Game Over - Tail
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
            await canvasContext.FillRectAsync(c * scaleFactor.c, r * scaleFactor.r, scaleFactor.c, scaleFactor.r);
        }

        public async Task drawGameState()
        {
            await canvasContext.BeginBatchAsync();

            await canvasContext.SetFillStyleAsync("red");
            await drawPiece(foodPosition.c, foodPosition.r);
            await clearTail();

            await canvasContext.SetFillStyleAsync("green");
            await drawPiece(snake.headPosition.c, snake.headPosition.r);

            await canvasContext.SetFillStyleAsync("darkgreen");
            for (int i = 0; i < snake.tail.Count; i++)
            {
                await drawPiece(snake.tail[i].pos.c, snake.tail[i].pos.r);
                snake.tail[i].val--;
            }
            await canvasContext.EndBatchAsync();
        }

    }

}
