using System.Numerics;
using System.Reflection;

namespace cmArcade.Shared;

public interface IGameObject
{
    List<GraphicAsset>? decals { get; set; }
    int healthPoints { get; set; }
    GraphicAsset model { get; set; }
    Vector2 pos { get; set; }
    int spriteSelect { get; set; }
    bool UpdatePosition((float row, float col) limits);
}
