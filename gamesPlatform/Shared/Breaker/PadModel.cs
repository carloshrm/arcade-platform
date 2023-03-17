namespace cmArcade.Shared.Breaker
{
    public class PadModel : GraphicAsset
    {
        public override string spriteId { get; set; } = string.Empty;
        public override int width { get; init; }
        public override int height { get; init; }

        public static readonly PadModel playerPad = new PadModel { spriteId = "player", width = 60, height = 10 };
    }
}
