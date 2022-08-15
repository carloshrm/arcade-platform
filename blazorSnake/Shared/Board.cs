﻿using Blazor.Extensions.Canvas.Canvas2D;

namespace blazorSnake.Shared
{
    public class Board
    {
        private Canvas2DContext canvasContext { get; set; }
        private (int r, int c) scaleOfset { get; set; }
        private (int row, int col) limits { get; set; }
        private (int r, int c) foodPosition { get; set; }
        public Snake snake { get; set; }

        public Board(int x, int y)
        {
            scaleOfset = (20, 20);
            limits = (y / scaleOfset.r, x / scaleOfset.c);
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
                row = rng.Next(1, limits.row - scaleOfset.r - 1);
                col = rng.Next(1, limits.col - scaleOfset.c - 1);

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
            if (snake.headPosition.r <= 0 ||
                snake.headPosition.c <= 0 ||
                snake.headPosition.r >= limits.row + scaleOfset.r ||
                snake.headPosition.c >= limits.col + scaleOfset.c)
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

        private async Task drawPiece(int c, int r)
        {
            await canvasContext.FillRectAsync(c, r, scaleOfset.c, scaleOfset.r);
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
                await drawPiece(0, i);
                await drawPiece((limits.col * scaleOfset.c) - scaleOfset.c, i);
            }
            for (int j = 0; j <= (limits.col * scaleOfset.c); j++)
            {
                await drawPiece(j, 0);
                await drawPiece(j, (limits.row * scaleOfset.r) - scaleOfset.r);
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
