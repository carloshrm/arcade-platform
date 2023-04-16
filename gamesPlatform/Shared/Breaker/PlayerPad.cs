﻿using System.Numerics;

namespace cmArcade.Shared.Breaker
{
    public class PlayerPad : IGameObject
    {
        public Vector2 pos { get; set; }
        public int healthPoints { get; set; }
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public Direction movingDir { get; set; }
        public float accel { get; set; }
        public float weight { get; set; }
        public List<GraphicAsset>? decals { get; set; } = null;

        public PlayerPad(int row, int col)
        {
            pos = new Vector2(col, row);
            model = PadModel.playerPad;
            movingDir = Direction.Zero;
            healthPoints = 3;
            accel = 0;
            weight = 0.6f;
            spriteSelect = 0;
        }

        public void setWeight(float w)
        {
            if (w > 0)
                weight = w;
            else
                throw new ArgumentException("weight should be positive");
        }

        public bool loseLife()
        {
            return --healthPoints <= 0;
        }

        public bool updatePosition((int row, int col) limits)
        {
            if (pos.X >= 0 && pos.X <= limits.col - model.width - 1)
            {
                pos = new Vector2(pos.X + accel, pos.Y);
            }
            else if (pos.X < 0)
            {
                pos = new Vector2(1, pos.Y);
            }
            else
            {
                pos = new Vector2(limits.col - model.width - 2, pos.Y);
            }

            if (movingDir == Direction.Right)
            {
                accel = accel < 6 ? accel + weight : 6;
            }
            else if (movingDir == Direction.Left)
            {
                accel = accel > -6 ? accel - weight : -6;
            }
            else
            {
                accel += accel > 0 ? -weight : weight;
                if (Math.Abs(accel) <= weight)
                    accel = 0;
            }
            return true;
        }
    }
}
