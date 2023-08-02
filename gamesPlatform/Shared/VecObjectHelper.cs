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
                Vector2.Distance(cPt + obj.pos, targetPos) <= Vector2.Distance(closestPoint, targetPos))
            {
                closestPoint = cPt + obj.pos;
            }
        }
        return closestPoint;
    }
}