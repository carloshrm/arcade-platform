﻿namespace cmArcade.Shared.Invaders
{
    public class ShipModel : GraphicAsset
    {
        public override string spriteId { get; set; }
        public override int width { get; init; }
        public override int height { get; init; }

        public static readonly ShipModel playerShip = new ShipModel { spriteId = "player", width = 60, height = 64 };

        public static readonly IReadOnlyDictionary<string, ShipModel> invaderShips = new Dictionary<string, ShipModel>
        {
            { "1" , new ShipModel { spriteId = "1", width = 28, height = 30 } },
            { "2" , new ShipModel { spriteId = "2", width = 36, height = 34 } },
            { "3" , new ShipModel { spriteId = "3", width = 30, height = 32 } },
            { "4" , new ShipModel { spriteId = "4", width = 38, height = 40 } },
            { "5" , new ShipModel { spriteId = "5", width = 55, height = 40 } },
        };
    };

}
