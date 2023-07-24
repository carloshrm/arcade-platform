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
    }

}
