using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class AsteroidModel : CanvasRenderedVectorial
{
    public override string lnColor { get; init; }
    public override float lnWidth { get; init; }
    public override IEnumerable<Vector2> points { get; set; }
    public override float objWidth { get; init; }
    public override float objHeight { get; init; }

    private AsteroidModel()
    {
        lnColor = "gray";
        lnWidth = 2f;
        objWidth = 10;
        objHeight = 10;
    }

    public static AsteroidModel GenerateRandomAsteroid()
    {
        var rng = new Random();
        var newPoints = new List<Vector2>();

        int pointCount = rng.Next(5, 8);
        var startingPt = new Vector2(0, rng.Next(3, 10));
        double angleDiv = 6.28 / (pointCount - 1);
        double angle = angleDiv;
        while (pointCount-- > 0)
        {
            double x = startingPt.X * Math.Cos(angle) - startingPt.Y * Math.Sin(angle);
            double y = startingPt.X * Math.Sin(angle) + startingPt.Y * Math.Cos(angle);
            var newPt = new Vector2((float)(x + rng.Next(0, 3)), (float)(y + rng.Next(0, 3)));
            newPoints.Add(newPt);
            angle += angleDiv;
        }
        return new AsteroidModel() { points = newPoints };
    }
}
