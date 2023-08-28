namespace cmArcade.Shared.Invaders;

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

    public static readonly IReadOnlyCollection<ShipModel> invaderShips = new List<ShipModel>
    {
        new ShipModel { spriteId = "ship1", width = 38, height = 36 },
        new ShipModel { spriteId = "ship2", width = 42, height = 34 },
        new ShipModel { spriteId = "ship3", width = 37, height = 32 },
        new ShipModel { spriteId = "ship4", width = 56, height = 50 },
        new ShipModel { spriteId = "ship5", width = 86, height = 40 },
    };
};
