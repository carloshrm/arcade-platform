namespace cmArcade.Shared.Breaker
{
    public class HealthPowerUp : IPowerUpEffect
    {
        public void runEffect(IGameField fieldReference)
        {
            fieldReference.getPlayer().healthPoints++;
            fieldReference.setMessage("+1 Life");
        }
    }
}