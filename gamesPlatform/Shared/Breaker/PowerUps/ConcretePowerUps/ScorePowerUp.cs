namespace cmArcade.Shared.Breaker
{
    public class ScorePowerUp : IPowerUpEffect
    {
        public void runEffect(IGameField t)
        {
            t.setScoreMultiplier(2);
        }
    }
}
