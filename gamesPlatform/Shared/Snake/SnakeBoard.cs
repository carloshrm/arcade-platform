using System.Numerics;

using Blazor.Extensions.Canvas.Canvas2D;

namespace cmArcade.Shared
{
    public class SnakeBoard : IGameField
    {
        private (int r, int c) limits { get; set; }
        public SnakePlayer player { get; private set; }
        public SnakeFood food { get; private set; }

        private string uiMessage { get; set; } = string.Empty;
        private int scoreMultipier = 0;

        public event EventHandler ateFood;
        private Canvas2DContext canvas;

        public SnakeBoard((int r, int c) limits, Canvas2DContext c)
        {
            this.limits = limits;
            player = new SnakePlayer(2, limits);
            ateFood += player.growSnake;
            ateFood += makeFood;
            canvas = c;
            makeFood(this, EventArgs.Empty);
        }

        public void setScoreMultiplier(int m)
        {
            scoreMultipier = m;
        }

        public void setMessage(String m)
        {
            uiMessage = m;
        }

        public void makeFood(Object? sender, EventArgs e)
        {
            var rng = new Random();
            Vector2 newPos;
            bool invalid;

            do
            {
                newPos = new Vector2(rng.Next(1, limits.r - 2), rng.Next(1, limits.c - 2));
                invalid = player.pos == newPos || player.tail.Exists(p => p.pos == newPos);
            } while (invalid);
            food = new SnakeFood(newPos);
        }

        public bool checkGameOver()
        {
            if (player.pos.Y < 0 || player.pos.X < 0 || player.pos.Y >= limits.r || player.pos.X >= limits.c)
                return true;
            else if (player.tail.Exists(tp => tp.pos == player.pos))
                return true;

            return false;
        }

        public void updateGameState(Score s)
        {
            player.tail.Add(new TailPiece(player.pos, player.healthPoints));
            player.updatePosition(limits);
            player.tail.RemoveAll(tp => tp.healthVal == 0);
            for (int i = 0; i < player.tail.Count; i++)
            {
                player.tail[i].healthVal--;
            }
        }

        public bool checkSnakeParts()
        {
            if (checkGameOver())
                return false;
            else
            {
                if (player.pos == food.pos)
                {
                    ateFood.Invoke(this, EventArgs.Empty);
                }
            }
            return true;
        }

        public Object getPlayer()
        {
            return player;
        }
    }
}
