using cmArcade.Shared.Breaker;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArcade.Shared.Asteroids;

public class ShipModel : CanvasRenderedAsset
{
    public override string color { get; init; }
    public override float width { get; init; }
    public override float height { get; init; }

    public static readonly ShipModel player = new ShipModel
    {
        color = "white",
        width = 10,
        height = 15,
    };
}
