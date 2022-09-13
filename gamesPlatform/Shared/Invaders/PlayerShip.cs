using System.Timers;
using Timer = System.Timers.Timer;

namespace cmArcade.Shared
{
    public class PlayerShip : GameActor
    {
        public override int healthPoints { get; set; }
        public override int row { get; set; }
        public override int col { get; set; }
        public override ShipModel model { get; set; }
        public Timer animator { get; set; }
        public int stepCount { get; set; }

        public Direction movingDir { get; set; }
        public double accel { get; set; }

        public bool canShoot { get; set; }

        public PlayerShip(int row, int col)
        {
            this.row = row;
            this.col = col;
            model = ShipModel.availableModels[0];
            animator = new Timer(100) { Enabled = true, AutoReset = true };
            animator.Elapsed += animate;
            stepCount = 0;
            movingDir = Direction.none;
            canShoot = true;
            healthPoints = 1;
            accel = 0;
        }

        public override bool updatePosition(int rowEdge, int colEdge)
        {
            if ((col >= 0 && movingDir == Direction.left) || (col <= colEdge - model.width - 1 && movingDir == Direction.right))
                col += (int)accel;

            if (movingDir == Direction.right)
                accel = 4;
            else if (movingDir == Direction.left)
                accel = -4;

            if (movingDir == Direction.none)
                accel = accel > 0 ? (accel - 0.01) : (accel + 0.01);

            return true;
        }

        public async Task shotTimeout()
        {
            canShoot = false;
            await Task.Delay(500);
            canShoot = true;
        }

        public void animate(Object? o, ElapsedEventArgs e)
        {
            if (movingDir == Direction.left)
                model.spriteSelect = 1;
            else if (movingDir == Direction.right)
                model.spriteSelect = 2;
            else
                model.spriteSelect = 0;

            stepCount = stepCount == 2 ? 0 : stepCount + 1;
        }

    }

}
