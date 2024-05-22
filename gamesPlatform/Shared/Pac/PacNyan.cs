using cmArcade.Shared.Pac.models;

using System.Numerics;

namespace cmArcade.Shared.Pac;

public class PacNyan(int x, int y) : IGameObject
{
    public bool powerUp { get; set; } = false;
    public int healthPoints { get; set; } = 2;

    public List<GraphicAsset>? decals { get; set; }
    public int spriteSelect { get; set; } = 1;
    public GraphicAsset model { get; set; } = NyanModel.GetModel();

    public Vector2 pos { get; set; } = new Vector2(x, y);
    public Vector2 movingDirection { get; set; } = VecDirection.Zero;

    public int speed { get; set; } = 1;

    public void SetDirection(Vector2 direction)
    {
        movingDirection = direction;
    }

    public bool UpdatePosition((float row, float col) limits)
    {

            pos += (movingDirection * speed);
            return true;
    }
}
