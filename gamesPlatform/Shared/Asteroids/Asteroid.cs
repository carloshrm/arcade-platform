using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class Asteroid : IGameObject
{
    public Vector2 pos { get; set; }
    public int healthPoints { get; set; }

    public List<GraphicAsset>? decals { get; set; }
    public GraphicAsset model { get; set; }
    public int spriteSelect { get; set; } = 1;

    public Asteroid((int row, int col) initialPos, int hp = 1)
    {
        pos = new Vector2(initialPos.col, initialPos.row);
        healthPoints = hp;
        model = AsteroidModel.asteroidModels.First();
    }

    public bool updatePosition((int row, int col) limits)
    {
        throw new NotImplementedException();
    }
}
