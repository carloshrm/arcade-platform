using System.Numerics;

namespace cmArcade.Shared
{
    public interface IGameObject
    {
        List<GraphicAsset>? decals { get; set; }
        int healthPoints { get; set; }
        GraphicAsset model { get; set; }
        Vector2 pos { get; set; }
        int spriteSelect { get; set; }
        bool updatePosition((int row, int col) limits);
    }
}