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
        objWidth = 30;
        objHeight = 30;
    }

    public static AsteroidModel GenerateRandomAsteroid()
    {
        var rng = new Random();
        var newPoints = new List<Vector2>();

        int pointCount = rng.Next(4, 10);
        var startingPt = new Vector2(0, rng.Next(15, 30));
        double angleDiv = 6.28 / pointCount;
        double angle = angleDiv;
        while (pointCount-- > 0) 
        {
            //create points by spinning around the initial pos offsetting the next one outwards by random
            double x = startingPt.X * Math.Cos(angle) - startingPt.Y * Math.Sin(angle);
            double y = startingPt.X * Math.Sin(angle) + startingPt.Y * Math.Cos(angle);
            var newPt = new Vector2((float)(x + rng.Next(10, 30)), (float)(y + rng.Next(10, 30)));
            newPoints.Add(newPt);
            angle += angleDiv;
        }
        return new AsteroidModel() { points = newPoints };
    }
}
