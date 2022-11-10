namespace cmArcade.Shared.Breaker
{
    public interface IPowerUpEffect
    {
        public void runEffect(object t);
        public static IPowerUpEffect getPowerUp(PowerUpType t)
        {
            switch (t)
            {
                case PowerUpType.health:
                    return new HealthPowerUp();
                case PowerUpType.ball:
                    return new BallPowerUp();
                default:
                    return null;
            }
        }
    }
}