using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class ShotModel : CanvasRenderedVectorial
{
    public ShotModel(IEnumerable<Vector2> points) : base(points) { }

    public override string strokeColor { get; set; } = "red";
    public override float strokeLineWidth { get; set; } = 4f;
    public override IEnumerable<Vector2> points { get; set; }
    public override float objWidth { get; set; } = 2f;
    public override float objHeight { get; set; } = 2f;
    public override string? fillColor { get; set; }
    public override Vector2 upRightBounds { get; set; }
    public override Vector2 bottomLeftBounds { get; set; }
}