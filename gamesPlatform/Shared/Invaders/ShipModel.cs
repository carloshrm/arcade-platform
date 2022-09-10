namespace cmArcade.Shared
{
    public class ShipModel
    {
        public int type { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int spriteSelect { get; set; } = 1;
        public int spriteRow { get; set; } = 0;

        public static readonly ShipModel[] availableModels = new ShipModel[]
        {
            new ShipModel { type = 0, width = 32, height = 40, spriteSelect = 0 },
            new ShipModel { type = 1, width = 80, height = 40, spriteSelect = 0 },
        };
    };

}
