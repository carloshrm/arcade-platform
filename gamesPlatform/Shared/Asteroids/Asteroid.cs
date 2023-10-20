using System.Diagnostics;
using System.Numerics;

using Timer = System.Timers.Timer;

namespace cmArcade.Shared.Asteroids;

public class Asteroid : ISimpleVectorialObject
{
    public CanvasRenderedVectorial model { get; set; }
    public Vector2 pos { get; set; }
    public bool wasHit { get; set; } = false;
    public bool isPrimary { get; set; }
    private float bumpLimit { get; set; }
    public Vector2 floatDir { get; set; }
    public bool canColide { get; set; } = false;

    public Asteroid(Vector2 pos, bool isPrimary = true)
    {
        this.pos = pos;
        this.isPrimary = isPrimary;
        model = AsteroidModel.GenerateRandomModel(isPrimary);
        floatDir = new Vector2((float)Random.Shared.NextDouble(), (float)Random.Shared.NextDouble());
        Task.Delay(1000).ContinueWith(_ => canColide = true);
    }

    public void UpdatePosition((float yEdge, float xEdge) limits)
    {
        pos += floatDir;
        if (pos.X < 0) 
            pos = new Vector2(limits.xEdge - 1, pos.Y);
        else if (pos.Y < 0) 
            pos = new Vector2(pos.X, limits.yEdge - 1);
        else if (pos.X >= limits.xEdge) 
            pos = new Vector2(1, pos.Y);
        else if (pos.Y >= limits.yEdge) 
            pos = new Vector2(pos.X, 1);

        if (bumpLimit > 0)
        {
            bumpLimit--;
        }
    }

    public void SetNormalizedFloatDir(Vector2 dir)
    {
        double normX = Math.Round(dir.X / dir.Length(), 2);
        double normY = Math.Round(dir.Y / dir.Length(), 2);

        if (double.IsNaN(normX))
            normX = Random.Shared.Next(0, 1);
        if (double.IsNaN(normY))
            normY = Random.Shared.Next(0, 1);

        floatDir = new Vector2((float)normX, (float)normY);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not Asteroid)
            return false;
        else
            return (int)pos.X == (int)((Asteroid)obj).pos.X
                && (int)pos.Y == (int)((Asteroid)obj).pos.Y;
    }

    public void Bump(Vector2 dir)
    {
        if (bumpLimit < 10)
        {
            SetNormalizedFloatDir(Vector2.Negate(floatDir) * dir);
            bumpLimit += 50;
        }
    }
}