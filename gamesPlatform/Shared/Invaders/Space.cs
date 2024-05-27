namespace cmArcade.Shared.Invaders
{
    public class Space : IGameField
    {
        private static Random rng = new Random();
        private int invaderShotCount = 0;
        public readonly int baseScore = 8;
        public double difficultyRatio = 1;
        public bool isNextRound = false;

        public (float row, float col) limits { get; set; }
        public PlayerShip player { get; set; }
        public List<InvaderShip> invaders { get; set; }
        public InvaderShip specialInvader { get; set; }
        public bool specialIsActive { get; set; }
        public List<FieldBarrier> barriers { get; set; }
        public List<LaserShot> shotsFired { get; set; }

        public string uiMessage { get; private set; } = string.Empty;

        public Space((float row, float col) limits)
        {
            player = new PlayerShip(limits.row - (limits.row / 10), limits.col / 2);
            shotsFired = new List<LaserShot>();
            invaders = new List<InvaderShip>();
            barriers = new List<FieldBarrier>();
            this.limits = limits;
            SetupCommonInvaders();
            specialInvader = SetupSpecialInvader();
            SetupBarriers();
        }

        public Object GetPlayer()
        {
            return player;
        }

        public void UpdateGameState(Score s)
        {
            hitDetection();
            player.UpdatePosition(limits);
            UpdateSpecialInvader();
            shotsFired.ForEach(s => s.UpdatePosition(limits));
        }

        public void SetupBarriers()
        {
            float row = limits.row * 0.80f;
            float col = limits.col / 6;
            barriers.Add(new FieldBarrier(row, col));
            barriers.Add(new FieldBarrier(row, limits.col - col));
            barriers.Add(new FieldBarrier(row, col * 3));
        }

        public void FireShot(IGameObject whoFired)
        {
            shotsFired.Add(new LaserShot(whoFired));
        }

        public void InvaderAttack()
        {
            if (rng.Next(10) >= 7 && invaderShotCount < (int)Math.Round(3 * difficultyRatio))
            {
                int j = invaders.Count - 1;
                var selected = invaders[j / 2];
                while (j >= 0)
                {
                    float fromLeft = Math.Abs(player.position.X - invaders[j].position.X);
                    if (fromLeft <= player.model.width)
                    {
                        selected = invaders[j];
                        break;
                    }
                    j--;
                }
                FireShot(selected);
                invaderShotCount++;
            }
        }

        public void SetupCommonInvaders()
        {
            int invadersPerRow = 10;
            int tallestInvader = ShipModel.invaderShips.Max(x => x.height) + 6;
            float rowPos = limits.row / 22;
            int colSize = (int)(limits.col / invadersPerRow * 0.7);
            foreach (var model in ShipModel.invaderShips.Take(4))
            {
                rowPos += tallestInvader;
                for (int j = 1; j <= invadersPerRow; j++)
                {
                    var colPos = (colSize * j) - (model.width / 2);
                    invaders.Add(new InvaderShip(rowPos, colPos, model));
                }
            }
        }

        private InvaderShip SetupSpecialInvader()
        {
            specialIsActive = false;
            var model = ShipModel.invaderShips.Last();
            return new InvaderShip(model.height, limits.col + model.width + 10, model);
        }

        private void UpdateSpecialInvader()
        {
            if (specialIsActive)
                specialInvader.position += VecDirection.Left * 3;
        }

        public void SpawnSpecialInvader()
        {
            if (invaders.Count % 9 == 0) specialIsActive = true;
            if (specialInvader.position.X <= 0 - specialInvader.model.width || specialInvader.healthPoints <= 0)
                specialInvader = SetupSpecialInvader();
        }

        public void ParseKeyDown(string input)
        {
            if (input.Equals(" ") || input.Equals("ArrowUp"))
            {
                if (player.canShoot)
                {
                    FireShot(player);
                    _ = player.shotTimeout();
                }
            }
            else
            {
                if (input.Equals("a") || input.Equals("ArrowLeft"))
                    player.movingDirection = VecDirection.Left;
                if (input.Equals("d") || input.Equals("ArrowRight"))
                    player.movingDirection = VecDirection.Right;
            }
        }

        public void ParseKeyUp(string input)
        {
            if (!input.Equals(" "))
                player.movingDirection = VecDirection.Zero;
        }

        public void UpdateInvaderState()
        {
            shotsFired.RemoveAll(s => s.position.Y <= 0 || s.position.Y >= limits.row || s.hitSomething);
            invaderShotCount = shotsFired.Count(x => !x.fromPlayer);

            bool touchedEdge = false;
            invaders.ForEach(i =>
            {
                if (i.UpdatePosition(limits)) touchedEdge = true;
                i.Animate();
            });

            if (touchedEdge)
            {
                invaders.ForEach(i =>
                {
                    i.FlipDirection();
                    i.DropRow(limits.row);
                    i.UpdatePosition(limits);
                });
            }
        }

        public bool CheckGameOver()
        {
            if (player.healthPoints <= 0)
                return true;
            else
            {
                foreach (var inv in invaders)
                {
                    if (inv.position.Y >= player.position.Y)
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
                    calculatedScore += baseScore * ((int)i.position.Y / 10);
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
            bool checkHit(IGameObject g, IGameObject s)
            {
                return
                    s.position.X >= g.position.X &&
                    s.position.X <= g.position.X + g.model.width &&
                    s.position.Y <= g.position.Y + g.model.height &&
                    s.position.Y > g.position.Y;
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

        public void ShowFieldMessage(string msg)
        {
            uiMessage = msg;
        }

        public void SetScoreMultiplier(int val)
        {
            difficultyRatio += val / 10.0;
        }
    }
}
