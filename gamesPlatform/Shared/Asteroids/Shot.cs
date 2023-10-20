using System.Numerics;
namespace cmArcade.Shared.Asteroids;

public class Shot : ISimpleVectorialObject
{
    public Vector2 pos { get; set; }
    public Vector2 dir { get; set; }

    public bool fade { get; set; } = false;

    public CanvasRenderedVectorial model { get; set; }


    public Shot(Vector2 pos, Vector2 dir)
    {
        this.pos = pos;
        this.dir = dir * 2.5f;
        Task.Delay(1400).ContinueWith(_ => fade = true);
        model = new ShotModel(new List<Vector2>() { dir, this.dir });
    }

    public void UpdatePosition()
    {
        pos += dir;
    }
}
