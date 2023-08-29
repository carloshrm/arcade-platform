namespace cmArcade.Shared.Breaker
{
    public class BallPowerUp : IPowerUpEffect
    {
        public void runEffect(IGameField fieldReference)
        {
            var field = (BreakerField)fieldReference;
            field.SetBall();
            field.balls.Last().Shoot();
            fieldReference.ShowFieldMessage("+1 Ball");
        }
    }
}