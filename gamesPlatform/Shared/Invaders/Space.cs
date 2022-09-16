namespace cmArcade.Shared
{
    public class Space
    {
        public const int baseScore = 8;

        private static Random rng = new Random();
        public PlayerShip player { get; set; }
        public List<LaserShot> shotsFired { get; set; }
        public List<InvaderShip> invaders { get; set; }
        public InvaderShip? specialInvader { get; set; }
        public int invaderStepCount { get; set; }

        private int invaderShotCount;

        public Space((int row, int col) limits)
        {
            player = new PlayerShip(limits.row - (int)Math.Round(limits.row * 0.10), limits.col / 2);
            shotsFired = new List<LaserShot>();
            invaders = new List<InvaderShip>();
            invaderShotCount = 0;
            invaderStepCount = 1;
            specialInvader = null;
            setupInvaders(limits);
        }
        public void fireShot(GameObject whoFired)
        {
            shotsFired.Add(new LaserShot(whoFired));
        }

        public void invaderAttack()
        {
            if (rng.Next(10) > 7 && invaderShotCount < 2)
            {
                foreach (var inv in invaders)
                {
                    if ((inv.col < player.col && player.movingDir == Direction.left) ||
                        (inv.col > player.col && player.movingDir == Direction.right))
                    {
                        fireShot(inv);
                        invaderShotCount++;
                        return;
                    }
                }
                fireShot(invaders[rng.Next(invaders.Count)]);
                invaderShotCount++;
            }
        }

        public void setupInvaders((int row, int col) limits)
        {
            int invadersPerRow = 12;
            int colSize = (int)(limits.col * 0.8 / invadersPerRow);
            for (int j = 1; j < ShipModel.availableModels.Length - 1; j++)
            {
                var model = ShipModel.availableModels[j];
                var rowPos = (model.spriteId * (limits.row / 14)) + model.height;

                for (int i = 1; i < invadersPerRow; i++)
                {
                    var colPos = (colSize * i) + (colSize - (model.width / 2));
                    invaders.Add(new InvaderShip(rowPos, colPos, model));
                }
            }
        }

        public void setupSpecialInvader((int row, int col) limits)
        {
            var model = ShipModel.availableModels.Last();
            var rowPos = model.height;
            var colPos = limits.col - model.width;
            specialInvader = new InvaderShip(rowPos, colPos, model);
        }

        public void updateSpecialInvader((int row, int col) limits)
        {
            if (specialInvader == null)
            {
                if (invaderStepCount % 9 == 0)
                {
                    setupSpecialInvader(limits);
                }
            }
            else
            {
                specialInvader.col -= 3;
                if (specialInvader.col <= 0)
                    specialInvader = null;
            }
        }

        public void parseKeyDown(string input, bool beingHeld)
        {
            if (input.Equals(" "))
            {
                if (player.canShoot is true)
                {
                    fireShot(player);
                    player.shotTimeout();
                }
            }
            else
            {
                if (input.Equals("a") || input.Equals("ArrowLeft"))
                    player.movingDir = Direction.left;
                if (input.Equals("d") || input.Equals("ArrowRight"))
                    player.movingDir = Direction.right;
            }
        }

        public void parseKeyUp(string input)
        {
            if (!input.Equals(" "))
                player.movingDir = Direction.none;
        }

        public void shotCleanup((int r, int c) limits)
        {
            shotsFired.RemoveAll(s => s.row <= 0 || s.row >= limits.r || s.hitSomething);
            invaderShotCount = shotsFired.Count(x => !x.fromPlayer);
        }

        public void hitDetection()
        {
            bool check(GameObject g, GameObject s)
            {
                return s.col >= g.col &&
                                s.col <= g.col + g.model.width &&
                                s.row <= g.row + g.model.height &&
                                s.row > g.row;
            }
            foreach (var shot in shotsFired)
            {
                if (!shot.hitSomething)
                {
                    if (shot.fromPlayer)
                    {
                        if (specialInvader != null && check(specialInvader, shot))
                        {
                            specialInvader.healthPoints--;
                            shot.hit();
                        }
                        foreach (var inv in invaders)
                        {
                            if (check(inv, shot))
                            {
                                inv.healthPoints--;
                                shot.hit();
                                Console.WriteLine("hit");
                            }
                        }
                    }
                    else
                    {
                        if (check(player, shot))
                        {
                            player.healthPoints--;
                            shot.hit();
                            Console.WriteLine("hit");
                        }
                    }
                }
            }
        }
    }
}
