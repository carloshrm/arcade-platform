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
    public override string lnColor { get; init; }
    public override float lnWidth { get; init; }
    public override IEnumerable<Vector2> points { get; set; }
    public override float objWidth { get; init; }
    public override float objHeight { get; init; }

    public static ShipModel GetHead() => new()
    {
        lnColor = "red",
        lnWidth = 2,
        points = new List<Vector2>
        {
            new Vector2(0, 14),
            new Vector2(10, 0),
            new Vector2(-10, 0),
        },
        objHeight = 14,
        objWidth = 10,
    };

    public static ShipModel GetHull() => new()
    {
        lnColor = "red",
        lnWidth = 2,
        points = new List<Vector2>
        {
            new Vector2(10, 10),
            new Vector2(-10, 10),
            new Vector2(-14, -10),
            new Vector2(-6, -2),
            new Vector2(0, -10),
            new Vector2(6, -2),
            new Vector2(14, -10),
        },
        objHeight = 10,
        objWidth = 14,
    };
}
