namespace cmArcade.Shared
{
    public class Space
    {
        public PlayerShip player;
        public List<LaserShot> shotsFired;
        public List<AlienShip> invaders;

        public Space((int row, int col) limits)
        {
            player = new PlayerShip(limits.row - (int)Math.Round(limits.row * 0.10), limits.col / 2);
            shotsFired = new List<LaserShot>();
            invaders = new List<AlienShip>();
            setupInvaders();
        }
        public void fireLazor(GameActor whoFired)
        {
            shotsFired.Add(new LaserShot(whoFired));
        }

        public void setupInvaders()
        {
            invaders.Add(new AlienShip(50, 400));
        }

        public void clearWreckedShips()
        {
            invaders.RemoveAll(i => i.healthPoints <= 0);
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
                        Console.WriteLine("checking");
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
                    if (shot.row < player.row) continue;

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
