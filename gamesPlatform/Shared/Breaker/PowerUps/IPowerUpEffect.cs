namespace cmArcade.Shared.Breaker
{
    public interface IPowerUpEffect
    {
        public void runEffect(IGameField fieldReference);
        public static IPowerUpEffect getBreakerPowerUp(BreakerPowerUpType t)
        {
            switch (t)
            {
                case BreakerPowerUpType.health:
                    return new HealthPowerUp();
                case BreakerPowerUpType.ball:
                    return new BallPowerUp();
                case BreakerPowerUpType.score:
                    return new ScorePowerUp();
                default:
                    return null;
            }
        }
    }
}