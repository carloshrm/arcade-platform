namespace cmArcade.Shared
{
    public class Space
    {
        public const int baseScore = 8;

        private static Random rng = new Random();
        public PlayerShip player { get; set; }
        public List<LaserShot> shotsFired { get; set; }
        public List<InvaderShip> invaders { get; set; }
        public InvaderShip specialInvader { get; set; }
        public bool specialIsActive { get; set; }
        public (int row, int col) limits { get; set; }

        public event EventHandler? gameOver;
        private int invaderShotCount;

        public Space((int row, int col) limits)
        {
            player = new PlayerShip(limits.row - (int)Math.Round(limits.row * 0.10), limits.col / 2);
            shotsFired = new List<LaserShot>();
            invaders = new List<InvaderShip>();
            invaderShotCount = 0;
            this.limits = limits;
            setupCommonInvaders();
            setupSpecialInvader();
        }

        public async Task updateGameState()
        {
            hitDetection();

            player.updatePosition(limits);
            updateSpecialInvader();
            shotsFired.ForEach(s => s.updatePosition(limits));
        }

        public void fireShot(GameObject whoFired)
        {
            shotsFired.Add(new LaserShot(whoFired));
        }

        public void invaderAttack()
        {
            if (rng.Next(10) > 6 && invaderShotCount < 2)
            {
                foreach (var inv in invaders.OrderByDescending(x => x.row))
                {
                    if (inv.col == player.col ||
                       (inv.col < player.col && player.movingDir == Direction.left) ||
                       (inv.col > player.col && player.movingDir == Direction.right))
                    {
                        fireShot(inv);
                        invaderShotCount++;
                        return;
                    }
                }
            }
        }

        public void setupCommonInvaders()
        {
            int invadersPerRow = 10;
            int tallestInvader = ShipModel.invaderShips.Max(x => x.height) + 10;
            int rowPos = limits.row / 20;
            int colSize = (int)(limits.col * 0.8 / invadersPerRow);
            for (int i = 0; i < 4; i++)
            {
                var model = ShipModel.invaderShips[i];
                rowPos += tallestInvader;

                for (int j = 1; j <= invadersPerRow; j++)
                {
                    var colPos = (colSize * j) + (colSize - (model.width / 2));
                    invaders.Add(new InvaderShip(rowPos, colPos, model));
                }
            }
        }

        private void setupSpecialInvader()
        {
            specialIsActive = false;
            var model = ShipModel.invaderShips.Last();
            specialInvader = new InvaderShip(model.height, limits.col + model.width + 10, model);
        }

        private void updateSpecialInvader()
        {
            if (specialIsActive)
                specialInvader.col -= 3;
            if (specialInvader.col <= 0 - specialInvader.model.width || specialInvader.healthPoints <= 0)
                setupSpecialInvader();
        }

        public void sendSpecial()
        {
            if (rng.Next(10) >= 9) specialIsActive = true;
        }

        public void parseKeyDown(string input)
        {
            if (input.Equals(" "))
            {
                if (player.canShoot)
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

        public void updateSpaceState()
        {
            shotsFired.RemoveAll(s => s.row <= 0 || s.row >= limits.row || s.hitSomething);
            invaderShotCount = shotsFired.Count(x => !x.fromPlayer);

            bool touchedEdge = false;
            invaders.ForEach(i =>
            {
                if (i.updatePosition(limits)) touchedEdge = true;
                i.animate();
            });

            if (touchedEdge)
            {
                InvaderShip.flipDirection();
                invaders.ForEach(i =>
                {
                    i.dropRow(limits.row);
                    i.updatePosition(limits);
                });
            }
            checkGameOver();
        }

        public void checkGameOver()
        {
            if (player.healthPoints <= 0) gameOver?.Invoke(this, EventArgs.Empty);
            else
            {
                foreach (var inv in invaders)
                {
                    if (inv.row >= player.row) gameOver?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public int invaderCleanup()
        {
            int calculatedScore = 0;
            invaders.RemoveAll(i =>
            {
                if (i.healthPoints <= 0)
                {
                    calculatedScore += baseScore * (i.row / 10);
                    return true;
                }
                else
                    return false;
            });
            if (specialInvader.healthPoints <= 0) calculatedScore += baseScore * 10;
            return calculatedScore;
        }

        public void hitDetection()
        {
            bool check(GameObject g, GameObject s)
            {
                return
                    s.col >= g.col &&
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
                        if (check(specialInvader, shot))
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
