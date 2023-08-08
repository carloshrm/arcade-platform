namespace cmArcade.Shared;

public interface IDrawable
{
    GraphicAsset model { get; set; }
    int spriteSelect { get; set; }
    List<GraphicAsset>? decals { get; set; }
}