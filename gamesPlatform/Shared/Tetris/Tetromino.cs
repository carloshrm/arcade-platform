using System.Numerics;

namespace cmArcade.Shared.Tetris
{
    public class Tetromino
    {
        public List<TetrominoPart> parts;
        private bool isHorizontal { get; set; }
        public Tetromino(int[][] shape)
        {
            parts = new List<TetrominoPart>(4);
            buildShapeFromMatrix(shape);
            isHorizontal = true;
        }

        private void buildShapeFromMatrix(int[][] shape)
        {
            for (int i = 0; i < shape.Length; i++)
            {
                for (int j = 0; j < shape[0].Length; j++)
                {
                    if (shape[i][j] != 0)
                    {
                        var newPiece = new TetrominoPart(new Vector2(j, i));
                        newPiece.isPivot = shape[i][j] == -1;
                        parts.Add(newPiece);
                    }
                }
            }
        }

        public void spin()
        {
            var pvt = parts.Find(part => part.isPivot).pos;

            // cos(t)   -sin(t) | x
            // sin(t)   cos(t)  | y

            // x` = x cos90 - y sin90
            // y' = x sin90 + y cos90

            // origin (a, b), cos90 = 0, sin90 = 1

            // x` = a - (y - b) 
            // y' = b + (x - a)

            lock (parts)
            {
                for (int i = 0; i < parts.Count; i++)
                {
                    Console.WriteLine("orig " + parts[i].pos);
                    if (!parts[i].isPivot)
                    {
                        double newX = pvt.X - (parts[i].pos.Y - pvt.Y);
                        double newY = pvt.Y + (parts[i].pos.X - pvt.X);
                        parts[i].pos = new Vector2((float)newX, (float)newY);
                    }
                    Console.WriteLine("n " + parts[i].pos);
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
