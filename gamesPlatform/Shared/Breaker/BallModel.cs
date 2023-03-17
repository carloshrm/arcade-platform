namespace cmArcade.Shared.Breaker
{
    public class BallModel : GraphicAsset
    {
        public override string spriteId { get; set; } = string.Empty;
        public override int width { get; init; }
        public override int height { get; init; }

        public static readonly BallModel breakerBall = new BallModel { spriteId = "ball", width = 10, height = 10 };
    }
}
