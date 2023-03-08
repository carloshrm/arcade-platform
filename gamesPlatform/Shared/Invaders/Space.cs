namespace cmArcade.Shared.Invaders
{
    public class Space
    {
        private static Random rng = new Random();
        private int invaderShotCount = 0;
        public const int baseScore = 8;
        public double difficultyRatio = 1;
        public bool isNextRound = false;

        public (int row, int col) limits { get; set; }
        public PlayerShip player { get; set; }
        public List<InvaderShip> invaders { get; set; }
        public InvaderShip specialInvader { get; set; }
        public bool specialIsActive { get; set; }
        public List<FieldBarrier> barriers { get; set; }
        public List<LaserShot> shotsFired { get; set; }

        public Space((int row, int col) limits)
        {
            player = new PlayerShip(limits.row - (limits.row / 10), limits.col / 2);
            shotsFired = new List<LaserShot>();
            invaders = new List<InvaderShip>();
            barriers = new List<FieldBarrier>();
            this.limits = limits;
            setupCommonInvaders();
            specialInvader = setupSpecialInvader();
            setupBarriers();
        }

        public void nextRound(int diffUp)
        {
            difficultyRatio += diffUp / 10.0;
        }

        public void updateGameState()
        {
            hitDetection();
            player.updatePosition(limits);
            updateSpecialInvader();
            shotsFired.ForEach(s => s.updatePosition(limits));
        }

        public void setupBarriers()
        {
            int row = (int)(limits.row * 0.80);
            int col = limits.col / 6;
            barriers.Add(new FieldBarrier(row, col));
            barriers.Add(new FieldBarrier(row, limits.col - col));
            barriers.Add(new FieldBarrier(row, col * 3));
        }

        public void fireShot(GameObject whoFired)
        {
            shotsFired.Add(new LaserShot(whoFired));
        }

        public void invaderAttack()
        {
            if (rng.Next(10) >= 7 && invaderShotCount < (int)Math.Round(3 * difficultyRatio))
            {
                int j = invaders.Count - 1;
                var selected = invaders[j / 2];
                while (j >= 0)
                {
                    int fromLeft = Math.Abs(player.col - invaders[j].col);
                    if (fromLeft <= player.model.width)
                    {
                        selected = invaders[j];
                        break;
                    }
                    j--;
                }
                fireShot(selected);
                invaderShotCount++;
            }
        }

        public void setupCommonInvaders()
        {
            int invadersPerRow = 10;
            int tallestInvader = ShipModel.invaderShips.Values.Max(x => x.height) + 6;
            int rowPos = limits.row / 22;
            int colSize = (int)(limits.col / invadersPerRow * 0.7);
            for (int i = 0; i < 4; i++)
            {
                var model = ShipModel.invaderShips[(i + 1).ToString()];
                rowPos += tallestInvader;
                for (int j = 1; j <= invadersPerRow; j++)
                {
                    var colPos = (colSize * j) - (model.width / 2);
                    invaders.Add(new InvaderShip(rowPos, colPos, model));
                }
            }
        }

        private InvaderShip setupSpecialInvader()
        {
            specialIsActive = false;
            var model = ShipModel.invaderShips.Last().Value;
            return new InvaderShip(model.height, limits.col + model.width + 10, model);
        }

        private void updateSpecialInvader()
        {
            if (specialIsActive)
                specialInvader.col -= 3;
        }

        public void sendSpecial()
        {
            if (invaders.Count % 9 == 0) specialIsActive = true;
            if (specialInvader.col <= 0 - specialInvader.model.width || specialInvader.healthPoints <= 0)
                specialInvader = setupSpecialInvader();
        }

        public void parseKeyDown(string input)
        {
            if (input.Equals(" ") || input.Equals("ArrowUp"))
            {
                if (player.canShoot)
                {
                    fireShot(player);
                    _ = player.shotTimeout();
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
        }

        public bool checkGameOver()
        {
            if (player.healthPoints <= 0)
                return true;
            else
            {
                foreach (var inv in invaders)
                {
                    if (inv.row >= player.row)
                        return true;
                }
                return false;
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
            if (specialInvader.healthPoints <= 0) calculatedScore += baseScore * 8;
            return calculatedScore;
        }

        public void hitDetection()
        {
            bool checkHit(GameObject g, GameObject s)
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
                    foreach (var barr in barriers)
                    {
                        if (barr.healthPoints > 0 && checkHit(barr, shot))
                        {
                            shot.hit();
                            barr.hit();
                        }
                    }

                    if (shot.fromPlayer)
                    {
                        if (checkHit(specialInvader, shot))
                        {
                            specialInvader.healthPoints--;
                            shot.hit();
                        }
                        foreach (var inv in invaders)
                        {
                            if (checkHit(inv, shot))
                            {
                                inv.healthPoints--;
                                shot.hit();
                            }
                        }
                    }
                    else
                    {
                        if (checkHit(player, shot))
                        {
                            player.healthPoints--;
                            shot.hit();
                        }
                    }
                }
            }
        }
    }
}
