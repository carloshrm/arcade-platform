using System.Net.WebSockets;
using System.Numerics;

using Timer = System.Timers.Timer;

namespace cmArcade.Shared.Asteroids;

public class PlayerShip
{
    private readonly float rotateAngle = (float)(Math.PI * 5 / 180);
    private bool isRotating = false;
    private bool rotateCw = false;
    private bool isThrusting = false;
    private bool movingFw = false;

    private ShipPart hull { get; set; }
    private ShipPart head { get; set; }
    private ShipPart jet { get; set; }

    private float momentum { get; set; } = 0;
    private Vector2 movingDir { get; set; }
    private readonly float decel = 0.06f;
    private readonly float accel = 0.4f;
    private readonly float maxSpeed = 6f;

    public int healthPoints { get; set; } = 3;
    public List<Shot> shots { get; set; }
    private Timer shotCooldown { get; init; }
    private readonly float cooldownVal = 350;

    public PlayerShip((float row, float col) initialPos)
    {
        hull = new ShipPart(initialPos.col, initialPos.row) { model = ShipModel.GetHull() };
        head = new ShipPart(initialPos.col, initialPos.row + hull.model.topRightBounds.Y) { model = ShipModel.GetHead() };
        jet = new ShipPart(initialPos.col, initialPos.row) { model = ShipModel.GetJet() };
        movingDir = Vector2.Zero;
        shots = new List<Shot>();
        shotCooldown = new Timer() { AutoReset = false, Enabled = false, Interval = cooldownVal };
    }

    public void Fire()
    {
        if (!shotCooldown.Enabled && shots.Count() <= 4)
        {
            shots.Add(new Shot(head.pos, head.pos - hull.pos));
            shotCooldown.Start();
        }
    }

    public void Thrust(bool fw = true)
    {
        isThrusting = true;
        movingFw = fw;
    }

    private void ApplyThrust()
    {
        if (isThrusting)
        {
            if (movingFw)
            {
                momentum = momentum < maxSpeed ? momentum + accel : maxSpeed;

                var scalar = momentum > 3 ? 0.05 : 0.5;
                movingDir = Vector2.Lerp(movingDir, Vector2.Normalize(hull.pos - head.pos), (float)scalar);
            }
            else
            {
                if (momentum > 0)
                    momentum -= accel;
            }
        }
    }

    public void StopThrust()
    {
        isThrusting = false;
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
        float angle = rotateCw ? rotateAngle : -rotateAngle;
        head.pos = RotateVector(hull.pos, head.pos, angle);
        head.model.points = head.model.points.Select(p => RotateVector(new Vector2(0), p, angle));
        hull.model.points = hull.model.points.Select(p => RotateVector(new Vector2(0), p, angle));
        jet.model.points = jet.model.points.Select(p => RotateVector(new Vector2(0), p, angle));
    }

    private Vector2 RotateVector(Vector2 rf, Vector2 vec, float angle)
    {
        Vector2 center = vec - rf;
        double x = center.X * Math.Cos(angle) - center.Y * Math.Sin(angle);
        double y = center.X * Math.Sin(angle) + center.Y * Math.Cos(angle);
        return new Vector2((float)x + rf.X, (float)y + rf.Y);
    }

    public void UpdatePosition((float row, float col) limits)
    {
        if (isRotating)
            ApplyRotation();

        if (isThrusting)
            ApplyThrust();

        var dirVec = Vector2.Multiply(movingDir, (float)momentum);
        head.pos -= dirVec;
        hull.pos -= dirVec;
        jet.pos -= dirVec;
        if (momentum != 0 && !isThrusting)
        {
            momentum += momentum > 0 ? -decel : +decel;
            if (momentum > 0 && momentum < 0.01)
                momentum = 0;
        }
        if (hull.pos.X < 0)
            WarpShip(new Vector2(limits.col - 1, hull.pos.Y));
        else if (hull.pos.Y < 0)
            WarpShip(new Vector2(hull.pos.X, limits.row - 1));
        else if (hull.pos.X >= limits.col)
            WarpShip(new Vector2(0, hull.pos.Y));
        else if (hull.pos.Y >= limits.row)
            WarpShip(new Vector2(hull.pos.X, 0));
    }

    private void WarpShip(Vector2 dest)
    {
        head.pos = new Vector2(dest.X + (head.pos.X - hull.pos.X), dest.Y + (head.pos.Y - hull.pos.Y));
        hull.pos = dest;
        jet.pos = hull.pos;
    }

    public void UpdateShots((float xEdge, float yEdge) limits)
    {
        shots.ForEach(s => s.UpdatePosition());
        shots.RemoveAll(s => s.fade ||
            s.pos.X <= 0 || s.pos.X >= limits.xEdge ||
            s.pos.Y <= 0 || s.pos.Y >= limits.yEdge);
    }

    public List<ShipPart> GetParts()
    {
        var parts = new List<ShipPart> { hull, head };
        if (isThrusting && movingFw)
            parts.Add(jet);

        return parts;
    }
}
