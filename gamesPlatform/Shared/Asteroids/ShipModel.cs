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
    public override string lnColor { get; set; }
    public override float lnWidth { get; set; }
    public override IEnumerable<Vector2> points { get; set; }
    public override float objWidth { get; set; }
    public override float objHeight { get; set; }

    public static ShipModel GetJet() => new()
    {
        lnColor = "orange",
        lnWidth = 4,
        points = new List<Vector2>
        {
            new Vector2(-2, 0),
            new Vector2(2, 0),
            new Vector2(0, -6),
        },
        objHeight = 4,
        objWidth = 4,
    };

    public static ShipModel GetHead() => new()
    {
        lnColor = "cyan",
        lnWidth = 2,
        points = new List<Vector2>
        {
            new Vector2(0, 16),
            new Vector2(8, -2),
            new Vector2(-8, -2),
        },
        objHeight = 16,
        objWidth = 8,
    };

    public static ShipModel GetHull() => new()
    {
        lnColor = "cyan",
        lnWidth = 2,
        points = new List<Vector2>
        {
            new Vector2(8, 8),
            new Vector2(-8, 8),
            new Vector2(-14, -8),
            new Vector2(-6, -2),
            new Vector2(0, 0),
            new Vector2(6, -2),
            new Vector2(14, -8),
        },
        objHeight = 10,
        objWidth = 14,
    };
}
