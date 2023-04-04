using System.Numerics;

namespace cmArcade.Shared.Tetris
{
    public class Tetromino
    {
        public List<TetrominoPart> parts;
        private bool isHorizontal { get; set; }
        public Tetromino(int centerPos, int[][] shape)
        {
            parts = new List<TetrominoPart>(4);
            buildShapeFromMatrix(centerPos, shape);
            isHorizontal = true;
        }

        private void buildShapeFromMatrix(int centerPos, int[][] shape)
        {
            for (int i = 0; i < shape.Length; i++)
            {
                for (int j = 0; j < shape[0].Length; j++)
                {
                    if (shape[i][j] != 0)
                    {
                        var newPiece = new TetrominoPart(new Vector2(j, i + centerPos));
                        newPiece.isPivot = shape[i][j] == -1;
                        parts.Add(newPiece);
                    }
                }
            }
        }

        public void step(Vector2 dir)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                parts[i].pos += dir;
            }
        }
    }
}
