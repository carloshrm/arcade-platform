using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class Asteroid : ISimpleVectorialObject
{
    public CanvasRenderedVectorial model { get; set; }
    public Vector2 pos { get; set; }

    public Vector2 floatDir { get; set; }    

    public Asteroid((int row, int col) initialPos, int hp = 1)
    {
        pos = new Vector2(initialPos.col, initialPos.row);
        model = AsteroidModel.GenerateRandomAsteroid();
    }

    public void UpdatePosition(int xEdge, int yEdge)
    {

    }

}
