
using System.Numerics;

namespace cmArcade.Shared;

public abstract class CanvasRenderedVectorial
{
    public abstract string lnColor { get; set; }
    public abstract float lnWidth { get; set; }
    public abstract IEnumerable<Vector2> points { get; set; }
    public abstract float objWidth { get; set; }
    public abstract float objHeight { get; set; }
}