using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class Asteroid : ISimpleVectorialObject
{
    public CanvasRenderedVectorial model { get; set; }
    public Vector2 pos { get; set; }

    public Vector2 floatDir { get; set; }    

    public Asteroid(Vector2 pos)
    {
        this.pos = pos;
        model = AsteroidModel.GenerateRandomAsteroid();
    }

    public void UpdatePosition(int xEdge, int yEdge)
    {

    }

}
