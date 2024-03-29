﻿using System.Numerics;

namespace cmArcade.Shared
{
    public class SnakeBoard : IGameField
    {
        private (int r, int c) limits { get; set; }
        public SnakePlayer player { get; private set; }
        public List<SnakeFood> food { get; private set; }
        private readonly int maxFood = 2;
        private string uiMessage { get; set; } = string.Empty;
        private int scoreMultipier = 0;

        public event EventHandler ateFood;

        public SnakeBoard((int r, int c) limits)
        {
            this.limits = limits;
            food = new List<SnakeFood>();
            player = new SnakePlayer(2, limits);
            ateFood += player.GrowSnake;
            ateFood += makeFood;
            makeFood(this, EventArgs.Empty);
        }

        public void SetScoreMultiplier(int m)
        {
            scoreMultipier = m;
        }

        public void ShowFieldMessage(String m)
        {
            uiMessage = m;
        }

        public void makeFood(Object? sender, EventArgs e)
        {
            var rng = new Random();
            Vector2 newPos;
            bool invalid;
            while (food.Count() <= maxFood)
            {
                do
                {
                    newPos = new Vector2(rng.Next(1, limits.r - 2), rng.Next(1, limits.c - 2));
                    invalid = player.pos == newPos || player.tail.Exists(p => p.pos == newPos);
                } while (invalid);
                food.Add(new SnakeFood(newPos));
            }
        }

        public bool CheckGameOver()
        {
            if (player.pos.Y < 0 || player.pos.X < 0 || player.pos.Y >= limits.r || player.pos.X >= limits.c)
                return true;
            else if (player.tail.Exists(tp => tp.pos == player.pos))
                return true;

            return false;
        }

        public void UpdateGameState(Score s)
        {
            player.tail.Add(new TailPiece(player.pos, player.healthPoints));
            player.UpdatePosition();
            player.tail.RemoveAll(tp => tp.healthVal == 0);
            for (int i = 0; i < player.tail.Count; i++)
            {
                player.tail[i].healthVal--;
            }
        }

        public bool checkSnakeParts()
        {
            if (CheckGameOver())
                return false;
            else
            {
                var chomp = food.Find(f => player.pos == f.pos);
                if (chomp != null)
                {
                    ateFood.Invoke(this, EventArgs.Empty);
                    food.Remove(chomp);
                }
            }
            return true;
        }

        public object GetPlayer()
        {
            return player;
        }
    }
}
