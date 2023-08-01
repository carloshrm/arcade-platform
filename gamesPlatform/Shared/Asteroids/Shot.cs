using System.Numerics;
using System.Timers;

using Timer = System.Timers.Timer;

namespace cmArcade.Shared.Asteroids;

public class Shot : ISimpleVectorialObject
{
    public Vector2 pos { get; set; }
    public Vector2 dir { get; set; }

    public bool fade { get; set; } = false;

    public CanvasRenderedVectorial model { get; set; }

    private Timer lifetime = new Timer() { Enabled = true, Interval = 2000 };

    public Shot(Vector2 pos, Vector2 dir)
    {
        this.pos = pos;
        this.dir = dir * 2.5f;
        lifetime.Elapsed += (o, e) => fade = true;
        model = new ShotModel() { points = new List<Vector2>() { dir, this.dir } };
    }

    public void UpdatePosition()
    {
        if (!fade)
        {
            pos += dir;
        }
    }
}
