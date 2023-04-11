using System.Numerics;

namespace cmArcade.Shared.Tetris
{
    public class Tetromino
    {
        public List<TetrominoPart> parts;

        public Tetromino(int centerPos, int[][] shape)
        {
            parts = new List<TetrominoPart>(4);
            buildShapeFromMatrix(centerPos, shape);
        }

        private void buildShapeFromMatrix(int centerPos, int[][] shape)
        {
            for (int i = 0; i < shape.Length; i++)
            {
                for (int j = 0; j < shape[0].Length; j++)
                {
                    if (shape[i][j] != 0)
                    {
                        var newPiece = new TetrominoPart(new Vector2(j + (centerPos - 1), i + 4));
                        newPiece.isPivot = shape[i][j] == -1;
                        parts.Add(newPiece);
                    }
                }
            }
            TetrominoPart.colorControl++;
        }

        public void step(Vector2 dir)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                parts[i].pos += dir;
            }
        }

        public void offsetHorzPos(int offVal)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                parts[i].pos = new Vector2(parts[i].pos.X - offVal, parts[i].pos.Y);
            }
        }
    }
}
