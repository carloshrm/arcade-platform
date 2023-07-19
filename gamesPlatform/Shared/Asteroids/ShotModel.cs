using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class ShotModel : CanvasRenderedVectorial
{
    public override string lnColor { get; init; } = "red";
    public override float lnWidth { get; init; } = 2f;
    public override IEnumerable<Vector2> points { get; set; }
    public override float objWidth { get; init; } = 2f;
    public override float objHeight { get; init; } = 2f;
}