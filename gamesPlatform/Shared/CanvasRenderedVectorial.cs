
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
    public abstract Vector2 upRightBounds { get; set; }
    public abstract Vector2 bottomLeftBounds { get; set; }

    protected CanvasRenderedVectorial(IEnumerable<Vector2> points)
    {
        this.points = points;

        float leftWidth = 1;
        float rightWidth = -1;
        float topHeight = -1;
        float bottomHeight = 1;

        foreach (var p in points)
        {
            if (p.X > rightWidth)
                rightWidth = p.X;
            else if (p.X < leftWidth)
                leftWidth = p.X;

            if (p.Y < bottomHeight)
                bottomHeight = p.Y;
            else if (p.Y > topHeight)
                topHeight = p.Y;
        }

        objWidth = rightWidth + Math.Abs(leftWidth);
        objHeight = topHeight + Math.Abs(bottomHeight);
        upRightBounds = new Vector2(rightWidth, topHeight);
        bottomLeftBounds = new Vector2(leftWidth, bottomHeight);
    }

}