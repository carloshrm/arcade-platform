namespace cmArcade.Shared.Breaker
{
    public class BallPowerUp : IPowerUpEffect
    {
        public void runEffect(object t)
        {
            var field = (BreakerField)t;
            field.setBall();
            field.balls.Last().shoot();
        }
    }
}