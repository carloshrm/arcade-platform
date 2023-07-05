
using System.Numerics;

namespace cmArcade.Shared;

public abstract class CanvasRenderedAsset
{
    public abstract string color { get; init; }
    public abstract float width { get; init; }
    public abstract float height { get; init; }
}

public abstract class CanvasRenderedVectorial
{
    public abstract string lnColor { get; init; }
    public abstract float lnWidth { get; init; }
    public abstract List<Vector2> points { get; init; }
    public abstract float objWidth { get; init; }
    public abstract float objHeight { get; init; }
}