namespace gamesPlatform.Shared
{
    public class Space
    {
        public PlayerShip player;
        public List<LaserShot> shotsFired;
        public List<AlienShip> invaders;

        public Space()
        {
            this.player = new PlayerShip();
            this.shotsFired = new List<LaserShot>();
            this.invaders = new List<AlienShip>();
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

        public void parseKeyDown(string input, bool beingHeld)
        {
            if (input.Equals(" ") && player.canShoot is true)
            {
                fireLazor(player);
                player.shotTimeout();
            }
            else
            {
                if (player.movingNow is not Direction.none && beingHeld is false)
                {
                    player.movingOverlap = player.movingNow;
                }
                if (input.Equals("a"))
                    player.movingNow = Direction.left;
                else if (input.Equals("d"))
                    player.movingNow = Direction.right;
            }
        }

        public void parseKeyUp(string input)
        {
            if (input.Equals(" "))
                return;
            else
            {
                if (player.movingOverlap is Direction.none)
                {
                    player.movingNow = Direction.none;
                }
                else
                {
                    player.movingNow = player.movingOverlap;
                    player.movingOverlap = Direction.none;
                }
            }
        }

    }
}
