namespace cmArcade.Shared.Breaker
{
    public class ScorePowerUp : IPowerUpEffect
    {
        public void runEffect(IGameField fieldReference)
        {
            fieldReference.SetScoreMultiplier(1);
            fieldReference.ShowFieldMessage("+1x Score Multiplier");
            Task.Delay(TimeSpan.FromSeconds(30)).ContinueWith(t => fieldReference.SetScoreMultiplier(-1));
        }
    }
}
