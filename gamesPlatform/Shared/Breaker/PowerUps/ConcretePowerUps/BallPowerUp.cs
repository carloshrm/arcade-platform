namespace cmArcade.Shared.Breaker
{
    public class BallPowerUp : IPowerUpEffect
    {

        public void runEffect(IGameField t)
        {
            var field = (BreakerField)t;
            field.setBall();
            field.balls.Last().shoot();
        }
    }
}