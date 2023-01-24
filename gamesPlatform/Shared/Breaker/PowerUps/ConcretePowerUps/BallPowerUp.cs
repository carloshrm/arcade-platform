namespace cmArcade.Shared.Breaker
{
    public class BallPowerUp : IPowerUpEffect
    {
        public void runEffect(IGameField fieldReference)
        {
            var field = (BreakerField)fieldReference;
            field.setBall();
            field.balls.Last().shoot();
            fieldReference.setMessage("+1 Ball");
        }
    }
}