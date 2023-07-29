
using System.Numerics;

namespace cmArcade.Shared;

public abstract class CanvasRenderedVectorial
{
    public abstract string strokeColor { get; set; }
    public abstract float strokeLineWidth { get; set; }
    public abstract string? fillColor { get; set; }
    public abstract IEnumerable<Vector2> points { get; set; }
    public abstract float objWidth { get; set; }
    public abstract float objHeight { get; set; }
}