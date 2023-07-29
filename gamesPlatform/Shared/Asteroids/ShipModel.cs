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

    public static ShipModel GetJet() => new()
    {
        strokeColor = "#e53b00",
        strokeLineWidth = 2,
        fillColor = "#e57300",
        points = new List<Vector2>
        {
            new Vector2(-6, 0),
            new Vector2(6, 0),
            new Vector2(0, -12),
        },
        objHeight = 4,
        objWidth = 4,
    };

    public static ShipModel GetHead() => new()
    {
        strokeColor = "#00A5E5",
        strokeLineWidth = 2,
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
        strokeColor = "#00A5E5",
        strokeLineWidth = 2,
        fillColor = " #5000E5",
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
