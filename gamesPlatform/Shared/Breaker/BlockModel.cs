namespace cmArcade.Shared.Breaker
{
    public class BlockModel : GraphicAsset
    {
        public override string spriteId { get; set; }
        public override int width { get; init; }
        public override int height { get; init; }
        public int HP { get; init; }

        public static readonly List<BlockModel> blocks = new List<BlockModel>
        {
            new BlockModel {spriteId = "simpleBlock", width = 80, height = 20, HP = 1 },
            new BlockModel {spriteId = "strongBlock", width = 80, height = 20, HP = 2 },
        };
    }
}
