namespace cmArcade.Shared.Asteroids;

public class AsteroidModel : GraphicAsset
{
    public override string spriteId { get; set; }
    public override int width { get; init; }
    public override int height { get; init; }

    public static readonly IReadOnlyList<AsteroidModel> asteroidModels = new List<AsteroidModel>
        {
            new AsteroidModel {spriteId = "asts", width = 10, height = 10 },
        };
}
