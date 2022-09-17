namespace cmArcade.Shared
{
    public class ShipModel : GameAsset
    {
        public override int spriteId { get; set; }
        public override int width { get; init; }
        public override int height { get; init; }
        public override int spriteSelect { get; set; }

        public static readonly ShipModel[] availableModels = new ShipModel[]
        {
            new ShipModel { spriteId = 0, width = 60, height = 64, spriteSelect = 0 },
            new ShipModel { spriteId = 1, width = 28, height = 30, spriteSelect = 0 },
            new ShipModel { spriteId = 2, width = 36, height = 34, spriteSelect = 0 },
            new ShipModel { spriteId = 3, width = 30, height = 32, spriteSelect = 0 },
            new ShipModel { spriteId = 4, width = 38, height = 40, spriteSelect = 0 },
            new ShipModel { spriteId = 5, width = 55, height = 40, spriteSelect = 0 },
        };
    };

}
