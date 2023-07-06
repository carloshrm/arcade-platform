using System.Numerics;

namespace cmArcade.Shared;

public interface ISimpleVectorialObject
{
    CanvasRenderedVectorial model { get; set; }
    Vector2 pos { get; set; }
}