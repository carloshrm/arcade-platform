namespace cmArcade.Shared.Tetris
{
    public static class TetrominoModel
    {
        public static readonly IReadOnlyCollection<int[][]> shapeList = new List<int[][]>
            {
                //triangle
                // 1 ia a block, -1 is center for spin
                new int[][] {
                        new int[] { 1, -1, 1 },
                        new int[] { 0, 1, 0 } },
                //straight
                new int[][] {
                        new int[] { -1, 1, 1, 1 }},
                //box
                new int[][] {
                        new int[] { -1, 1, },
                        new int[] { 1, 1, } },
                //stairs
                new int[][] {
                        new int[] { -1, 1, 0 },
                        new int[] { 0, 1, 1 } },
                //stairs mirrored
                new int[][] {
                        new int[] { 0, 1, 1 },
                        new int[] { -1, 1, 0 } },
                //L
                new int[][] {
                        new int[] { -1, 1, 1 },
                        new int[] { 1, 0, 0 } },
                //L mirrored
                new int[][] {
                        new int[] { -1, 1, 1 },
                        new int[] { 0, 0, 1 } },
            };
    }
}
