namespace cmArcade.Shared.Breaker
{
    public class PadModel : GraphicAsset
    {
        public override string spriteId { get; set; }
        public override int width { get; init; }
        public override int height { get; init; }

        public static readonly PadModel playerPad = new PadModel { spriteId = "pad", width = 60, height = 10 };
    }
}
