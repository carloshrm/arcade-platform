using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class ShipPart : ISimpleGameObject
{
    public CanvasRenderedAsset model { get; set; } = ShipModel.player;
    public Vector2 pos { get; set; }

    public ShipPart(float x, float y)
    {
        pos = new Vector2(x, y);
    }
}
