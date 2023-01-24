namespace cmArcade.Shared.Breaker
{
    public class ScorePowerUp : IPowerUpEffect
    {
        public void runEffect(IGameField fieldReference)
        {
            fieldReference.setScoreMultiplier(1);
            fieldReference.setMessage("+1x Score Multiplier");
            Task.Delay(TimeSpan.FromSeconds(30)).ContinueWith(t => fieldReference.setScoreMultiplier(-1));
        }
    }
}
