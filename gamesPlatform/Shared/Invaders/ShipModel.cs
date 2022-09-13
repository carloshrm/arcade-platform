namespace cmArcade.Shared
{
    public record ShipModel
    {
        public int type { get; set; }
        public int width { get; init; }
        public int height { get; init; }
        public int spriteSelect { get; set; }
        public int spriteRow { get; init; } = 0;

        public static readonly ShipModel[] availableModels = new ShipModel[]
        {
            new ShipModel { type = 0, width = 30, height = 28, spriteSelect = 0, spriteRow = 0 },
            new ShipModel { type = 1, width = 80, height = 40, spriteSelect = 0, spriteRow = 0 },
            new ShipModel { type = 2, width = 32, height = 40, spriteSelect = 0, spriteRow = 1 },
            new ShipModel { type = 3, width = 32, height = 40, spriteSelect = 0, spriteRow = 1 },
        };
    };

}
