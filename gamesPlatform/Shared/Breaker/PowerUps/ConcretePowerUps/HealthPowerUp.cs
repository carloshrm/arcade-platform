namespace cmArcade.Shared.Breaker
{
    public class HealthPowerUp : IPowerUpEffect
    {
        public void runEffect(IGameField fieldReference)
        {
            (fieldReference.GetPlayer() as PlayerPad).healthPoints++;
            fieldReference.ShowFieldMessage("+1 Life");
        }
    }
}