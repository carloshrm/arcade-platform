namespace cmArcade.Shared.Breaker
{
    public class BlockModel : GraphicAsset
    {
        public override string spriteId { get; set; }
        public override int width { get; init; }
        public override int height { get; init; }

        public static readonly BlockModel block = new BlockModel { spriteId = "blocks", width = 80, height = 20 };
    }
}
