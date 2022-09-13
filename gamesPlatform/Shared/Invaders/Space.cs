namespace cmArcade.Shared
{
    public class Space
    {
        public const int baseScore = 8;

        private static Random rng = new Random();
        public PlayerShip player { get; set; }
        public List<LaserShot> shotsFired { get; set; }
        public List<AlienShip> invaders { get; set; }

        private int invaderShotCount;

        public Space((int row, int col) limits)
        {
            player = new PlayerShip(limits.row - (int)Math.Round(limits.row * 0.10), limits.col / 2);
            shotsFired = new List<LaserShot>();
            invaders = new List<AlienShip>();
            invaderShotCount = 0;
            setupInvaders(limits);
        }
        public void fireLazor(GameActor whoFired)
        {
            shotsFired.Add(new LaserShot(whoFired));
        }

        public void invaderAttack()
        {
            if (rng.Next(10) > 7)
            {
                if (invaderShotCount < 2)
                {
                    bool fired = false;
                    foreach (var inv in invaders)
                    {
                        if ((inv.col < player.col && player.movingDir == Direction.left) ||
                            (inv.col > player.col && player.movingDir == Direction.right))
                        {
                            fireLazor(inv);
                            fired = true;
                        }
                        if (fired)
                        {
                            invaderShotCount++;
                            return;
                        }
                    }
                    if (!fired)
                    {
                        fireLazor(invaders[rng.Next(invaders.Count)]);
                        invaderShotCount++;
                    }
                }
            }
        }

        public void setupInvaders((int row, int col) limits)
        {
            int invadersPerRow = 6;
            var colSize = limits.col * 0.8 / invadersPerRow;
            for (int j = 1; j < ShipModel.availableModels.Length; j++)
            {
                var model = ShipModel.availableModels[j];
                var rowPos = (model.type * (limits.row / 8)) + model.height;

                for (int i = 1; i < invadersPerRow; i++)
                {
                    var colPos = (colSize * i) + (colSize - (model.width / 2));
                    invaders.Add(new AlienShip(rowPos, (int)colPos, model));
                }
            }
        }

        public void parseKeyDown(string input, bool beingHeld)
        {
            if (input.Equals(" "))
            {
                if (player.canShoot is true)
                {
                    fireLazor(player);
                    player.shotTimeout();
                }
            }
            else
            {
                if (input.Equals("a") || input.Equals("ArrowLeft"))
                    player.movingDir = Direction.left;
                else if (input.Equals("d") || input.Equals("ArrowRight"))
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
            shotsFired.RemoveAll(s => s.row <= 0 || s.row >= limits.r || s.hit);
            invaderShotCount = shotsFired.Count(x => !x.fromPlayer);
        }

        public void hitCheck()
        {
            foreach (var shot in shotsFired)
            {
                if (shot.fromPlayer)
                {
                    foreach (var inv in invaders)
                    {
                        if (shot.row > inv.row + inv.model.height) continue;
                        if (shot.col >= inv.col && shot.col <= inv.col + inv.model.width)
                        {
                            inv.healthPoints--;
                            shot.hit = true;
                            Console.WriteLine("hit");
                            break;
                        }
                    }
                }
                else
                {
                    if (shot.row < player.row + player.model.height) continue;

                    if (shot.col >= player.col && shot.col + LaserShot.length <= shot.col + player.model.width)
                    {
                        player.healthPoints--;
                        shot.hit = true;
                        Console.WriteLine("hit");
                    }
                }

            }
        }

    }
}
