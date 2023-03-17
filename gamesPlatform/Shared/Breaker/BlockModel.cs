namespace cmArcade.Shared.Breaker
{
    public class BlockModel : GraphicAsset
    {
        public override string spriteId { get; set; } = string.Empty;
        public override int width { get; init; }
        public override int height { get; init; }
        public int HP { get; init; }
        public bool isSpecial { get; init; }

        public static readonly IReadOnlyCollection<BlockModel> blocks = new List<BlockModel>
        {
            new BlockModel {spriteId = "blocks simple", width = 80, height = 20, HP = 1, isSpecial = false },
            new BlockModel {spriteId = "blocks strong", width = 80, height = 20, HP = 2, isSpecial = false },
            new BlockModel {spriteId = "blocks fragile", width = 80, height = 20, HP = 1, isSpecial = true },
            new BlockModel {spriteId = "blocks hollow", width = 80, height = 20, HP = 2, isSpecial = true },
        };

        public const int variationCount = 5;
        public static readonly int widestBlockSize = blocks.Max(x => x.width);
        public static readonly int highestBlockSize = blocks.Max(x => x.height);
    }
}
