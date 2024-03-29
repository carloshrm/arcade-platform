﻿using System.Numerics;

namespace cmArcade.Shared.Breaker
{
    public class Ball : IGameObject
    {
        public Vector2 pos { get; set; }
        public int healthPoints { get; set; }
        public GraphicAsset model { get; set; }
        public int spriteSelect { get; set; }
        public List<GraphicAsset>? decals { get; set; }
        private Vector2 movementVector { get; set; } = Vector2.Zero;

        public bool breakingTimeout = false;
        private bool bouncingTimeout = false;

        public Ball(float row, float col)
        {
            model = BallModel.breakerBall;
            pos = new Vector2(col - (model.width / 2), row - 10);
            healthPoints = 0;
            spriteSelect = 0;
            movementVector = new Vector2(0, 0);
        }

        public void Follow(float c)
        {
            pos = new Vector2(c - (model.width / 2), pos.Y);
        }

        public void Shoot()
        {
            movementVector = new Vector2(0, -4);
        }

        public void LockoutBreaks()
        {
            if (!breakingTimeout)
            {
                breakingTimeout = true;
                Task.Delay(TimeSpan.FromMilliseconds(50)).ContinueWith((t) => breakingTimeout = false);
            }
        }

        public async void Bounce(int rDir, int cDir)
        {
            if (!bouncingTimeout)
            {
                bouncingTimeout = true;
                movementVector = new Vector2(movementVector.X * cDir, movementVector.Y * rDir);
                // TODO - slow down?
                await Task.Delay(50);
                bouncingTimeout = false;
            }
        }

        public void OffsetVector(float accel)
        {
            movementVector = new Vector2((float)Math.Floor(accel / -10), movementVector.Y);
        }

        public bool UpdatePosition((float row, float col) limits)
        {
            if (pos.Y <= limits.row)
            {
                pos += movementVector;
                return true;
            }
            else
                return false;
        }
    }
}
