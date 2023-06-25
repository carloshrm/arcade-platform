using cmArcade.Shared.Breaker;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArcade.Shared.Asteroids;

public class ShipModel : GraphicAsset
{
    public override string spriteId { get; set; }
    public override int width { get; init; }
    public override int height { get; init; }

    public static readonly ShipModel player = new ShipModel
    {
        spriteId = "assship",
        width = 10,
        height = 30,
    };
}
