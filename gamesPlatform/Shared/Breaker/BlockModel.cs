namespace cmArcade.Shared.Breaker
{
    public class BlockModel : GraphicAsset
    {
        public override string spriteId { get; set; } = string.Empty;
        public override int width { get; init; }
        public override int height { get; init; }
        public int HP { get; init; }
        public bool isSpecial { get; init; }

        public static readonly IReadOnlyList<BlockModel> blocks = new List<BlockModel>
        {
            new BlockModel {spriteId = "simpleBlock", width = 80, height = 20, HP = 1, isSpecial = false },
            new BlockModel {spriteId = "strongBlock", width = 80, height = 20, HP = 2, isSpecial = false },
            new BlockModel {spriteId = "fragileBlock", width = 80, height = 20, HP = 1, isSpecial = true },
            new BlockModel {spriteId = "hollowBlock", width = 80, height = 20, HP = 2, isSpecial = true },
        };

        public const int variationCount = 5;
        public static readonly int widestBlockSize = blocks.Max(x => x.width);
        public static readonly int highestBlockSize = blocks.Max(x => x.height);
    }
}
