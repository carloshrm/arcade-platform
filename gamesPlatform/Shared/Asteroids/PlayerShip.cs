using System.Net.WebSockets;
using System.Numerics;

using Timer = System.Timers.Timer;

namespace cmArcade.Shared.Asteroids;

public class PlayerShip
{
    private readonly double rotateAngle = Math.PI * 2 / 180;
    private bool isRotating = false;
    private bool rotateCw = false;

    private ShipPart hull { get; set; }
    private ShipPart head { get; set; }

    private double momentum { get; set; } = 0;
    private Vector2 movingDir { get; set; }
    private readonly double decel = 0.02;
    private readonly double accel = 0.08;
    private readonly double maxSpeed = 4;

    public int healthPoints { get; set; } = 3;
    public List<Shot> shots { get; set; }

    private Timer shotCooldown { get; init; }
    private readonly double cooldownVal = 350;

    public PlayerShip((int row, int col) initialPos)
    {
        hull = new ShipPart(initialPos.col, initialPos.row) { model = ShipModel.GetHull() };
        head = new ShipPart(initialPos.col, initialPos.row + hull.model.objHeight) { model = ShipModel.GetHead() };
        movingDir = new Vector2(0, 0);
        shots = new List<Shot>();
        shotCooldown = new Timer() { AutoReset = false, Enabled = false, Interval = cooldownVal };
    }

    public void Fire()
    {
        if (!shotCooldown.Enabled && shots.Count() <= 4)
        {
            shots.Add(new Shot() { pos = head.pos, dir = head.pos - hull.pos });
            shotCooldown.Start();
        }
    }

    public void Thrust(bool fw = true)
    {
        if (fw)
        {
            if (momentum < maxSpeed)
                momentum += accel;

            var dir = Vector2.Normalize(movingDir - (head.pos - hull.pos));
            movingDir = Vector2.Normalize(Vector2.Add(dir, movingDir));
        } else
        {
            if (momentum > 0)
                momentum -= accel;
        }
    }

    public void Rotate(bool cw = false)
    {
        rotateCw = cw;
        isRotating = true;
    }

    public void StopRotation()
    {
        isRotating = false;
    }

    private void ApplyRotation()
    {
        // cos(t)   -sin(t) | x
        // sin(t)   cos(t)  | y
        double angle = rotateCw ? rotateAngle : -rotateAngle;
        head.pos = RotateVector(hull.pos, head.pos, angle);
        head.model.points = head.model.points.Select(p => RotateVector(new Vector2(0), p, angle));
        hull.model.points = hull.model.points.Select(p => RotateVector(new Vector2(0), p, angle));
    }

    private Vector2 RotateVector(Vector2 rf, Vector2 vec, double angle)
    {
        Vector2 center = vec - rf;
        double x = center.X * Math.Cos(angle) - center.Y * Math.Sin(angle);
        double y = center.X * Math.Sin(angle) + center.Y * Math.Cos(angle);
        return new Vector2((float)x + rf.X, (float)y + rf.Y);
    }

    public void updatePosition((int row, int col) limits)
    {
        if (isRotating)
            ApplyRotation();

        var dirVec = Vector2.Multiply(movingDir, (float)momentum);
        head.pos -= dirVec;
        hull.pos -= dirVec;

        if (momentum != 0)
        {
            momentum += momentum > 0 ? -decel : +decel;
            if (momentum > 0 && momentum < 0.01)
                momentum = 0;
        }
    }

    public void UpdateShots(int xEdge, int yEdge)
    {
        shots.ForEach(s => s.UpdatePosition());
        shots.RemoveAll(s => s.fade || 
            s.pos.X <= 0 || s.pos.X >= xEdge ||
            s.pos.Y <= 0 || s.pos.Y >= yEdge);
    }

    public List<ShipPart> getParts()
    {
        return new List<ShipPart>() { hull, head };
    }
}
