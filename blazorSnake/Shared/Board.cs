using Blazor.Extensions.Canvas.Canvas2D;

namespace blazorSnake.Shared
{
    public class Board
    {
        private Canvas2DContext canvasContext { get; set; }
        private (int r, int c) scaleOfset { get; set; }
        private (int row, int col) limits { get; set; }
        private (int r, int c) foodPosition { get; set; }
        public Snake snake { get; set; }

        public Board(int rows, int cols)
        {
            scaleOfset = (20, 20);
            limits = (rows / scaleOfset.r, cols / scaleOfset.c);
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
            if (snake.headPosition.r <= 0 || snake.headPosition.c <= 0 || snake.headPosition.r >= limits.row || snake.headPosition.c >= limits.col)
            {
                Console.WriteLine("\n Game Over edge.");
                return false;
            }
            else
            {
                foreach (var tp in snake.tail)
                {
                    if (tp.pos == snake.headPosition)
                    {
                        Console.WriteLine("\n Game Over tail.");

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

        private async Task drawPiece(int r, int c)
        {
            await canvasContext.FillRectAsync(r, c, scaleOfset.c, scaleOfset.r);
        }

        public async Task drawGameState()
        {
            await canvasContext.BeginBatchAsync();

            await canvasContext.SetFillStyleAsync("red");
            await drawPiece(foodPosition.c * scaleOfset.c, foodPosition.r * scaleOfset.r);
            foreach (var tailPiece in snake.tail)
            {
                await canvasContext.SetFillStyleAsync("#ffffff");
                if (tailPiece.val <= 0)
                {
                    await drawPiece(tailPiece.pos.c * scaleOfset.c, tailPiece.pos.r * scaleOfset.r);
                }
            }
            snake.tail.RemoveAll(tp => tp.val == 0);

            await canvasContext.SetFillStyleAsync("green");
            await drawPiece(snake.headPosition.c * scaleOfset.c, snake.headPosition.r * scaleOfset.r);

            await canvasContext.SetFillStyleAsync("darkgreen");
            for (int i = 0; i < snake.tail.Count; i++)
            {
                await drawPiece(snake.tail[i].pos.c * scaleOfset.c, snake.tail[i].pos.r * scaleOfset.r);
                Console.WriteLine(snake.tail[i].val);
                snake.tail[i].val--;
            }
            await canvasContext.EndBatchAsync();
        }

        public async Task drawBoardEdges()
        {
            await canvasContext.BeginBatchAsync();
            await canvasContext.SetFillStyleAsync("black");
            for (int i = 0; i <= (limits.row * scaleOfset.r); i++)
            {
                await canvasContext.FillRectAsync(i, 0, scaleOfset.c, scaleOfset.r);
                await drawPiece(i, 0);
                await drawPiece(i, (limits.col * scaleOfset.c) - scaleOfset.c);
            }
            for (int j = 0; j <= (limits.col * scaleOfset.c); j++)
            {
                await drawPiece(0, j);
                await drawPiece((limits.row * scaleOfset.r) - scaleOfset.r, j);
            }
            await canvasContext.EndBatchAsync();
        }

        public async void drawGameOver()
        {
            await canvasContext.BeginBatchAsync();
            await canvasContext.SetFillStyleAsync("black");
            await canvasContext.FillRectAsync(0, limits.row * scaleOfset.r / 2.5, limits.col * scaleOfset.c, limits.row * 3);
            await canvasContext.SetFillStyleAsync("white");
            await canvasContext.SetFontAsync("50px Comic Sans");
            await canvasContext.SetTextAlignAsync(TextAlign.Center);
            await canvasContext.FillTextAsync("Game Over", limits.col * scaleOfset.c / 2.0, limits.row * scaleOfset.r / 2.0);
            await canvasContext.EndBatchAsync();
        }

        public void newPlayer(Snake p)
        {
            snake = p;
            snake.setStartingValues(limits.row, limits.col);
        }

    }
}
