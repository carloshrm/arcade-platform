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

        public void setupInvaders((int row, int col) limits)
        {
            var perRow = (double)limits.col / ShipModel.availableModels[1].width / 2;
            for (int i = 1; i < perRow; i++)
            {
                invaders.Add(new AlienShip(limits.row / 10, (ShipModel.availableModels[1].width + (ShipModel.availableModels[1].width / 2)) * i, 1));
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
