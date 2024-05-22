using System.Numerics;

namespace cmArcade.Shared.Pac;

public class PacMaze : IGameObject
{
    public bool[][] collisionMap { get; set; }
    public GraphicAsset model { get; set; }
    public List<(int x, int y)> powerUps { get; set; }

    public List<GraphicAsset>? decals { get; set; }
    public int healthPoints { get; set; } = -1;
    public Vector2 pos { get; set; } = Vector2.Zero;
    public int spriteSelect { get; set; } = 1;

    public PacMaze()
    {
        model = PacMazeModel.testMaze;
        //var test = File.ReadLines("./test.txt");
        collisionMap = (new bool[1280][]).Select(l => l = new bool[720]).ToArray();
    }

    public bool UpdatePosition((float row, float col) limits)
    {
        throw new NotImplementedException();
    }
}

public class PacMazeModel : GraphicAsset
{
    public override string spriteId { get; set; }
    public override int width { get; init; }
    public override int height { get; init; }

    public static readonly PacMazeModel testMaze = new()
    {
        spriteId = "test-maze",
        width = 1280,
        height = 720
    };
}
