using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class Asteroid : ISimpleVectorialObject
{
    public CanvasRenderedVectorial model { get; set; }
    public Vector2 pos { get; set; }

    public bool wasHit { get; set; } = false;
    public bool isPrimary { get; set; }

    public Vector2 floatDir { get; set; }    

    public Asteroid(Vector2 pos, bool isPrimary = true)
    {
        this.pos = pos;
        this.isPrimary = isPrimary;
        model = AsteroidModel.GenerateRandomAsteroid(isPrimary);
        floatDir = new Vector2((float)Random.Shared.NextDouble(), (float)Random.Shared.NextDouble());
    }

    public void UpdatePosition(int xEdge, int yEdge)
    {
        pos += floatDir;
        if (pos.X < 0) pos = new Vector2(xEdge - 1, pos.Y);
        else if (pos.Y < 0) pos = new Vector2(pos.X, yEdge);
        else if (pos.X >= xEdge) pos = new Vector2(0, pos.Y);
        else if (pos.Y >= yEdge) pos = new Vector2(pos.X, 0);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not Asteroid)
            return false;
        else
            return (int)pos.X == (int)((Asteroid)obj).pos.X 
                && (int)pos.Y == (int)((Asteroid)obj).pos.Y;
    }   

}