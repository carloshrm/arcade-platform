namespace cmArcade.Shared
{
    public class ShipModel : GameAsset
    {
        public override string spriteId { get; set; }
        public override int width { get; init; }
        public override int height { get; init; }

        public static readonly ShipModel playerShip = new ShipModel { spriteId = "p", width = 60, height = 64 };

        public static readonly ShipModel[] invaderShips = new ShipModel[]
        {
            new ShipModel { spriteId = "1", width = 28, height = 30 },
            new ShipModel { spriteId = "2", width = 36, height = 34 },
            new ShipModel { spriteId = "3", width = 30, height = 32 },
            new ShipModel { spriteId = "4", width = 38, height = 40 },
            new ShipModel { spriteId = "5", width = 55, height = 40 },
        };
    };

}
