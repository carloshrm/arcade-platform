namespace cmArcade.Shared.Invaders
{
    public class ShipModel : GraphicAsset
    {
        public override string spriteId { get; set; } = string.Empty;
        public override int width { get; init; }
        public override int height { get; init; }

        public static readonly ShipModel playerShip = new ShipModel
        {
            spriteId = "player",
            width = 60,
            height = 64
        };

        public static readonly IReadOnlyDictionary<string, ShipModel> invaderShips = new Dictionary<string, ShipModel>
        {
            {"1", new ShipModel { spriteId = "1", width = 38, height = 36 } },
            {"2", new ShipModel { spriteId = "2", width = 42, height = 34 } },
            {"3", new ShipModel { spriteId = "3", width = 37, height = 32 } },
            {"4", new ShipModel { spriteId = "4", width = 56, height = 50 } },
            {"5", new ShipModel { spriteId = "5", width = 86, height = 40 } },
        };
    };

}
