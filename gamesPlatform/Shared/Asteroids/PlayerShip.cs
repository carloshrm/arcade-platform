using System.Net.WebSockets;
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

public class PlayerShip
{
    private double rotateAngle = Math.PI * 45 / 180;
    public ShipPart hull { get; set; }
    public ShipPart head { get; set; }

    public double momentum { get; set; } = 0;
    public readonly double decel = 0.2;
    public readonly double accel = 0.4;

    public int healthPoints { get; set; } = 3;

    public PlayerShip((int row, int col) initialPos)
    {
        hull = new ShipPart(initialPos.col, initialPos.row);
        head = new ShipPart(initialPos.col, initialPos.row + hull.model.height);
    }

    public void Thrust(bool fw = true)
    {
        if (momentum < 6 && momentum > -6)
            momentum += fw ? accel : -accel;
    }

    public void Rotate()
    {
        Vector2 piv = head.pos - hull.pos;
        double x = piv.X * Math.Cos(rotateAngle) - piv.Y * Math.Sin(rotateAngle);
        double y = piv.X * Math.Sin(rotateAngle) + piv.Y * Math.Cos(rotateAngle);
        head.pos = new Vector2((float)x + hull.pos.X, (float)y + hull.pos.Y);
    }

    public void updatePosition((int row, int col) limits)
    {
        var dirVec = Vector2.Multiply(head.pos - hull.pos, (float)momentum);
        Console.WriteLine("dir :" + dirVec.X + " -- " + dirVec.Y);
        head.pos += dirVec;
        hull.pos += dirVec;

        if (momentum != 0)
        {
            momentum += momentum > 0 ? -decel : +decel;
            if (momentum > 0 && momentum < 0.1)
                momentum = 0;
        }
    }

    public List<ShipPart> getParts()
    {
        return new List<ShipPart>() { hull, head };
    }
}
