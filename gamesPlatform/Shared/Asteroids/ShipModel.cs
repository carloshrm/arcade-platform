using cmArcade.Shared.Breaker;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace cmArcade.Shared.Asteroids;

public class ShipModel : CanvasRenderedVectorial
{
    public override string strokeColor { get; set; }
    public override float strokeLineWidth { get; set; }
    public override IEnumerable<Vector2> points { get; set; }
    public override float objWidth { get; set; }
    public override float objHeight { get; set; }
    public override string? fillColor { get; set; } = null;
    public override Vector2 topRightBounds { get; set; }
    public override Vector2 bottomLeftBounds { get; set; }

    public ShipModel(IEnumerable<Vector2> points) : base(points) { }

    public static ShipModel GetJet() => new(new List<Vector2>
        {
            new Vector2(-6, 0),
            new Vector2(6, 0),
            new Vector2(0, -12)
        })
    {
        strokeColor = "#e53b00",
        strokeLineWidth = 2,
        fillColor = "#e57300",
    };

    public static ShipModel GetHead() => new(new List<Vector2>
        {
            new Vector2(0, 16),
            new Vector2(8, -2),
            new Vector2(-8, -2),
        })
    {
        strokeColor = "#50AAE5",
        strokeLineWidth = 2,
        fillColor = " #4000A0",
    };

    public static ShipModel GetHull() => new(
        new List<Vector2>
        {
            new Vector2(8, 8),
            new Vector2(-8, 8),
            new Vector2(-14, -8),
            new Vector2(-6, -2),
            new Vector2(0, 0),
            new Vector2(6, -2),
            new Vector2(14, -8),
        })
    {
        strokeColor = "#50AAE5",
        strokeLineWidth = 2,
        fillColor = " #4000A0",
    };
}
