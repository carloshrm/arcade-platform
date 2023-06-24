using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class PlayerShip
{
    public Vector2 pos { get; set; }
    public int healthPoints { get; set; }
    public GraphicAsset model { get; set; }

    public PlayerShip((int row, int col) initialPos)
    {
        pos = new Vector2(initialPos.col, initialPos.row);
        healthPoints = 3;
    }
}
