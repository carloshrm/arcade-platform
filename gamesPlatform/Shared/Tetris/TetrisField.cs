using System.Numerics;

namespace cmArcade.Shared.Tetris
{
    public class TetrisField : IGameField
    {
        private readonly (int row, int col) limits;
        private TetrisPlayer player { get; set; }
        public ITetrisElement[][] field { get; private set; }
        public Tetromino activePiece { get; set; }
        public Queue<Tetromino> nextPieces { get; private set; }
        public int activeEdges { get; private set; }

        public string uiMessage { get; set; } = string.Empty;

        public int scoreMult { get; set; } = 0;
        private int baseScore = 3;

        public TetrisField((float row, float col) limits)
        {
            this.limits = ((int row, int col))limits;
            activeEdges = (int)(limits.col / 2 / 2);
            player = new TetrisPlayer();
            field = new ITetrisElement[this.limits.row][].Select(_ => new ITetrisElement[this.limits.col]).ToArray();
            activePiece = getRandomTetromino();
            nextPieces = new Queue<Tetromino>(new[] { getRandomTetromino(), getRandomTetromino() });
            activePiece.offsetHorzPos(activeEdges + 4);
            setupEdges();
        }

        private void setupEdges()
        {
            for (int i = 0; i < limits.row; i++)
            {
                field[i][activeEdges] = new TetrisFieldEdge(new Vector2(activeEdges, i));
                field[i][limits.col - activeEdges] = new TetrisFieldEdge(new Vector2(limits.col - activeEdges, i));
            }
        }

        private Tetromino getRandomTetromino()
        {
            return new Tetromino(limits.col - (activeEdges / 2), TetrominoModel.shapeList.ElementAt(new Random().Next(0, TetrominoModel.shapeList.Count)));
        }

        public Object GetPlayer()
        {
            return player;
        }

        public void ShowFieldMessage(string msg)
        {
            uiMessage = msg;
        }

        public void SetScoreMultiplier(int val)
        {
            scoreMult = val;
        }

        public bool CheckGameOver()
        {
            return activePiece.parts.Any(p =>
            {
                return field[(int)p.pos.Y][(int)p.pos.X] != null;
            });
        }

        public void spin()
        {
            var pvt = activePiece.parts.Find(part => part.isPivot).pos;

            var prevState = new Vector2[activePiece.parts.Count];
            for (int i = 0; i < activePiece.parts.Count; i++)
            {
                float newX = pvt.X - (activePiece.parts[i].pos.Y - pvt.Y);
                float newY = pvt.Y + (activePiece.parts[i].pos.X - pvt.X);
                Vector2 newPos = new Vector2(newX, newY);

                if (field[(int)newPos.Y][(int)newPos.X] != null || newPos.X < 0 || newPos.Y < 0)
                {
                    while (i-- > 0)
                        activePiece.parts[i].pos = prevState[i];
                    return;
                }

                prevState[i] = activePiece.parts[i].pos;
                activePiece.parts[i].pos = newPos;
            }
        }

        public void parseKeyDown(string input)
        {
            switch (input)
            {
                case "ArrowUp":
                case "w":
                    spin();
                    break;
                case "ArrowDown":
                case "s":
                    if (!checkBottomCollision(activePiece)) activePiece.step(VecDirection.Down);
                    break;
                case "ArrowLeft":
                case "a":
                    if (!checkLeftCollision(activePiece)) activePiece.step(VecDirection.Left);
                    break;
                case "ArrowRight":
                case "d":
                    if (!checkRightCollision(activePiece)) activePiece.step(VecDirection.Right);
                    break;
                case " ":
                    snapActiveToBottom();
                    break;
                default:
                    break;
            }
        }

        private void snapActiveToBottom()
        {
            while (!checkBottomCollision(activePiece))
            {
                activePiece.step(VecDirection.Down);
            }

        }

        public void parseKeyUp(string input)
        {
            return;
        }

        private bool checkLeftCollision(Tetromino t)
        {
            return t.parts.Any(p => p.pos.X - 1 < 0 || field[(int)p.pos.Y][(int)(p.pos.X - 1)] != null);
        }

        private bool checkRightCollision(Tetromino t)
        {
            return t.parts.Any(p => p.pos.X + 1 == limits.col || field[(int)p.pos.Y][(int)(p.pos.X + 1)] != null);
        }

        private bool checkBottomCollision(Tetromino t)
        {
            return t.parts.Any(p => p.pos.Y + 1 >= limits.row || field[(int)p.pos.Y + 1][(int)p.pos.X] != null);
        }

        public void UpdateGameState(Score s)
        {
            if (checkBottomCollision(activePiece) is false)
                activePiece.step(VecDirection.Down);
            else
                settleActive();

            int completedLines = searchLines();
            scoreMult += completedLines;
            s.scoreValue += baseScore * scoreMult * completedLines;
        }

        private void settleActive()
        {
            activePiece.parts.ForEach(p => field[(int)p.pos.Y][(int)p.pos.X] = p);
            activePiece = nextPieces.Dequeue();
            activePiece.offsetHorzPos(activeEdges + 4);
            nextPieces.Enqueue(getRandomTetromino());
        }

        private int searchLines()
        {
            int lineCount = 0;
            for (int i = 0; i < field.Length; i++)
            {
                bool lineFormed = true;
                for (int j = activeEdges; j < limits.col - activeEdges; j++)
                {
                    if (field[i][j] == null)
                    {
                        lineFormed = false;
                        break;
                    }
                }
                // TODO - search for block colors 
                if (lineFormed)
                {
                    lineCount++;
                    dropLine(i);
                }

            }
            return lineCount;
        }

        private void dropLine(int line)
        {
            int k = line;
            while (k > 0)
            {
                for (int j = activeEdges + 1; j < limits.col - activeEdges; j++)
                {
                    field[k][j] = field[k - 1][j];
                    if (field[k][j] != null)
                        field[k][j].pos = new Vector2(j, k);
                }
                k--;
            }
        }
    }
}