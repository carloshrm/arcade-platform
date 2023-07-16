using System.Numerics;
using System.Timers;

using Timer = System.Timers.Timer;

namespace cmArcade.Shared.Asteroids;

public class Shot
{
    public Vector2 pos { get; set; }
    public Vector2 dir { get; set; }

    public bool fade { get; set; } = false;
    private Timer lifetime = new Timer() { Enabled = true, Interval = 2000 };

    public Shot()
    {
        lifetime.Elapsed += (o, e) => fade = true;
    }

    public void UpdatePosition()
    {
        pos += dir;
    }
}
