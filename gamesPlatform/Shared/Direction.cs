using System.Numerics;

namespace cmArcade.Shared
{
    public enum Direction
    {
        Left = -1,
        Zero = 0,
        Right = 1
    }

    public static class VecDirection
    {
        public static readonly Vector2 Up = new Vector2(0, -1);
        public static readonly Vector2 Down = new Vector2(0, 1);
        public static readonly Vector2 Left = new Vector2(1, 0);
        public static readonly Vector2 Right = new Vector2(-1, 0);
    }
}
