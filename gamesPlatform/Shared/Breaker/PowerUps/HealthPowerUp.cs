namespace cmArcade.Shared.Breaker
{
    public class HealthPowerUp : IPowerUpEffect
    {
        public void runEffect(object t)
        {
            ((PlayerPad)t).healthPoints++;
        }
    }
}