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
    public override List<Vector2> points { get; init; }
    public override float objWidth { get; init; }
    public override float objHeight { get; init; }

    public static readonly ShipModel player = new ShipModel
    {
        lnColor = "green",
        lnWidth = 4,
        points = new List<Vector2>
        {
            new Vector2(10, 0),
            new Vector2(-10, 0),
            new Vector2(0, 10),
            new Vector2(0, -10),
        },
        objHeight = 10,
        objWidth = 10,
    };
}
