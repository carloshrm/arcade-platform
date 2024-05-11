using System.Numerics;

namespace cmArcade.Shared.Pac;

public class PacNyan : IGameObject
{
    public bool powerUp { get; set; }
    public int healthPoints { get; set; }

    public List<GraphicAsset>? decals { get; set; }
    public int spriteSelect { get; set; }
    public GraphicAsset model { get; set; }

    public Vector2 pos { get; set; }
    public Vector2 movingDirection { get; set; }

    public PacNyan(int x, int y)
    {
        pos = new Vector2(x, y);
        healthPoints = 2;
        powerUp = false;
        movingDirection = VecDirection.Zero;
    }

    public void moveTo(Vector2 direction)
    {
        movingDirection = direction;
    }

    public bool UpdatePosition((float row, float col) limits)
    {
        pos += movingDirection;
        return true;
    }
}
