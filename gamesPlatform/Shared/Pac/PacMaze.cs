using System.Numerics;

namespace cmArcade.Shared.Pac;

public class PacMaze : ISimpleVectorialObject
{
    public bool[][] collisionMap { get; set; }
    public List<(int x, int y)> powerUps { get; set; }
    public Vector2 pos { get; set; } = Vector2.Zero;
    public CanvasRenderedVectorial model { get; set; }

    public PacMaze()
    {
        model = PacMazeModel.GetTestMaze();
        //var test = File.ReadLines("./test.txt");
        collisionMap = (new bool[1280][]).Select(l => l = new bool[720]).ToArray();
        foreach (var pt in model.points)
        {
            for (int i = pt; i < length; i++)
            {
                
            }
        }
    }
}

public class PacMazeModel(IEnumerable<Vector2> points) : CanvasRenderedVectorial(points)
{
    public override string strokeColor { get; set; }
    public override float strokeLineWidth { get; set; }
    public override string? fillColor { get; set; }
    public override IEnumerable<Vector2> points { get; set; }
    public override float objWidth { get; set; }
    public override float objHeight { get; set; }
    public override Vector2 topRightBounds { get; set; }
    public override Vector2 bottomLeftBounds { get; set; }

    public static PacMazeModel GetTestMaze() => 
        new(new List<Vector2> 
        { 
            new Vector2(200, 700),
            new Vector2(600, 700),
            new Vector2(600, 500),
            new Vector2(800, 500),
            new Vector2(800, 700),
            new Vector2(1000, 700),
        })
    {
        strokeColor = "#00FF00",
        strokeLineWidth = 3,
        fillColor = "#AF00F0",
    };
}
