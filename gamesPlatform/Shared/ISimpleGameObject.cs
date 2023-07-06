using System.Numerics;

namespace cmArcade.Shared;

public interface ISimpleGameObject
{
    CanvasRenderedAsset model { get; set; }
    Vector2 pos { get; set; }
}
