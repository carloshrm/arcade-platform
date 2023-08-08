using System.Numerics;

namespace cmArcade.Shared;

public static class VecObjectHelper
{
    public static Vector2 FindClosestPoint(this ISimpleVectorialObject obj, Vector2 targetPos)
    {
        Vector2 closestPoint = Vector2.Zero;
        foreach (var cPt in obj.model.points)
        {
            if (closestPoint == Vector2.Zero ||
                closestPoint.CompareDistanceTo(targetPos))
            {
                closestPoint = cPt + obj.pos;
            }
        }
        return closestPoint;
    }

    public static bool CompareDistanceTo(this Vector2 obj, Vector2 target)
    {
        return (obj.X * obj.X) + (obj.Y * obj.Y) > (target.X * target.X) + (target.Y * target.Y);
    }

    public static float GetManhattanDistanceTo(this Vector2 obj, Vector2 target) 
    { 
        return Math.Abs(obj.X - target.X) + Math.Abs(obj.Y - target.Y);
    }
}