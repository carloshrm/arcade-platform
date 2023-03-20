using System.Numerics;

namespace cmArcade.Shared.Tetris
{
    public class Tetromino
    {
        public List<Vector2> parts;
        public string color = "white";

        public Tetromino(int[][] shape)
        {
            parts = new List<Vector2>(4);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (shape[i][j] != 0)
                        parts.Add(new Vector2(j, i));
                }
            }

        }

        public void step()
        {
            for (int i = 0; i < parts.Count; i++)
            {
                parts[i] += VecDirection.Down;
            }
        }
    }

    public class TetrominoModel
    {
        public static readonly IReadOnlyCollection<int[][]> shapeList = new List<int[][]>
            {
                new int[][] {
                    new int[] { 1, 1, 1, 0 },
                    new int[] { 0, 1, 0, 0 } },
            };
    }



    public class TetrisField : IGameField
    {
        public (int r, int c) scaleFactor { get; set; } = (1, 1);

        private readonly (int row, int col) limits;
        private TetrisPlayer player { get; set; }
        private int[][] field { get; set; }
        public Tetromino active { get; set; }

        public string uiMessage { get; set; } = "TODO";

        public TetrisField((int row, int col) limits)
        {
            this.limits = limits;
            player = new TetrisPlayer();
            field = new int[limits.row][].Select(_ => new int[limits.col]).ToArray();
            active = new Tetromino(TetrominoModel.shapeList.First());
        }


        public Object getPlayer()
        {
            return player;
        }

        public void setMessage(string msg)
        {
            uiMessage = msg;
        }

        public void setScoreMultiplier(int val)
        {
            throw new NotImplementedException();
        }

        public bool checkGameOver()
        {
            return false;
        }

        public void parseKeyDown(string input)
        {
            Console.WriteLine(input);
        }

        public void parseKeyUp(string input)
        {
            Console.WriteLine(input);
        }

        public void updateGameState()
        {
            Console.WriteLine("update state");
            active.step();
            // move blocks
            // find lines
            // spawn a new block
        }
    }
}
