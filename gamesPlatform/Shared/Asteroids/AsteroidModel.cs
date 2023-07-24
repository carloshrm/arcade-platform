using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class AsteroidModel : CanvasRenderedVectorial
{
    public override string lnColor { get; set; }
    public override float lnWidth { get; set; }
    public override IEnumerable<Vector2> points { get; set; }
    public override float objWidth { get; set; }
    public override float objHeight { get; set; }

    private AsteroidModel()
    {
        lnColor = "gray";
        lnWidth = 2f;
        objWidth = 0;
        objHeight = 0;
    }

    public static AsteroidModel GenerateRandomAsteroid(bool isPrimary)
    {
        var newPoints = new List<Vector2>();

        int pointCount = Random.Shared.Next(6, 10);
        var startingPt = new Vector2(isPrimary ? 20 : 0, Random.Shared.Next(10, isPrimary ? 60 : 20));
        double angleDiv = 6.28 / --pointCount;
        double angle = angleDiv;

        while (pointCount-- > 0)
        {
            //create points by spinning around the initial pos offsetting the next one outwards by random
            double x = startingPt.X * Math.Cos(angle) - startingPt.Y * Math.Sin(angle);
            double y = startingPt.X * Math.Sin(angle) + startingPt.Y * Math.Cos(angle);
            var newPt = new Vector2(
                (float)(x + (x * Random.Shared.NextDouble())),
                (float)(y + (y * Random.Shared.NextDouble())));
            newPoints.Add(newPt);
            angle += angleDiv;
        }
        return new AsteroidModel() { points = newPoints };
    }
}
