using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class Shot
{
    public Vector2 pos { get; set; }
    public Vector2 dir { get; set; }

    public bool fade { get; set; } = false;
    private System.Timers.Timer lifetime = new System.Timers.Timer() { Enabled = true, Interval = 2000 };

    public Shot()
    {
        lifetime.Elapsed += (o, e) => fade = true;
    }

    public void UpdatePosition()
    {
        pos += Vector2.Multiply(dir, 2);
    }
}
