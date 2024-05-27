using System.Numerics;

namespace cmArcade.Shared;

public interface IGameObject
{
    int healthPoints { get; set; }

    List<GraphicAsset>? decals { get; set; }
    GraphicAsset model { get; set; }
    int spriteSelect { get; set; }

    Vector2 position { get; set; }
    Vector2 movingDirection { get; set; }
    float movingSpeed { get; set; }

    bool UpdatePosition((float row, float col) limits);
}
