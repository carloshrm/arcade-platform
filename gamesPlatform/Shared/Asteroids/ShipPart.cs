using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class ShipPart : ISimpleVectorialObject
{
    public Vector2 pos { get; set; }

    public CanvasRenderedVectorial model { get; set; }

    public ShipPart(float x, float y)
    {
        pos = new Vector2(x, y);
    }
}
