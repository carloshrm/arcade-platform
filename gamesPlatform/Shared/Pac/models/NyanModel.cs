namespace cmArcade.Shared.Pac.models;

public class NyanModel : GraphicAsset
{
    public override string spriteId { get; set; }
    public override int width { get; init; }
    public override int height { get; init; }

    public static NyanModel GetModel()
    {
        return new NyanModel { spriteId = "nyan", width = 10, height = 10 };
    }
}
