using System.Numerics;

namespace cmArcade.Shared.Tetris
{
    public class TetrisField : IGameField
    {
        private readonly (int row, int col) limits;
        private TetrisPlayer player { get; set; }
        public ITetrisElement[][] field { get; private set; }
        public Tetromino active { get; set; }
        public Queue<Tetromino> next { get; private set; }
        public int activeEdges { get; private set; }


        private bool holdingDown = false;
        private CancellationTokenSource snapDownToken = new CancellationTokenSource();

        public string uiMessage { get; set; } = string.Empty;

        private int scoreMult = 1;
        private int baseScore = 1;

        public TetrisField((int row, int col) limits)
        {
            this.limits = limits;
            activeEdges = limits.col / 2 / 2;
            player = new TetrisPlayer();
            field = new ITetrisElement[limits.row][].Select(_ => new ITetrisElement[limits.col]).ToArray();
            active = getRandomTetromino();
            next = new Queue<Tetromino>(new[] { getRandomTetromino(), getRandomTetromino() });
            active.offsetHorzPos(activeEdges + 4);
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
            scoreMult = val;
        }

        public bool checkGameOver()
        {
            return field[0].Any(p =>
            {
                if (p != null && p is not TetrisFieldEdge) return p.pos.Y <= 0;
                else return false;
            });
        }

        public void spin()
        {
            // Using an R2 rotation matrix, but with the origin on the -1 block from the model shape array

            // cos(t)   -sin(t) | x
            // sin(t)   cos(t)  | y

            // simplify with cos90deg = 0, sin90deg = 1
            // x` = x cos90 - y sin90
            // y' = x sin90 + y cos90

            // origin (a, b)
            // x` = a - (y - b) 
            // y' = b + (x - a)

            var pvt = active.parts.Find(part => part.isPivot).pos;

            var prevState = new Vector2[active.parts.Count];
            for (int i = 0; i < active.parts.Count; i++)
            {
                float newX = pvt.X - (active.parts[i].pos.Y - pvt.Y);
                float newY = pvt.Y + (active.parts[i].pos.X - pvt.X);
                Vector2 newPos = new Vector2(newX, newY);

                if (field[(int)newPos.Y][(int)newPos.X] != null || newPos.X < 0)
                {
                    while (i-- > 0)
                        active.parts[i].pos = prevState[i];
                    return;
                }

                prevState[i] = active.parts[i].pos;
                active.parts[i].pos = newPos;
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
                    if (!checkBottomCollision(active))
                    {
                        active.step(VecDirection.Down);
                        if (!holdingDown)
                        {
                            holdingDown = true;
                            Task.Delay(800).ContinueWith(_ =>
                            {
                                snapActiveToBottom();
                            }, snapDownToken.Token);
                        }
                    }
                    break;
                case "ArrowLeft":
                case "a":
                    if (!checkLeftCollision(active))
                        active.step(VecDirection.Left);
                    break;
                case "ArrowRight":
                case "d":
                    if (!checkRightCollision(active))
                        active.step(VecDirection.Right);
                    break;
                default:
                    break;
            }
        }

        private async Task snapActiveToBottom()
        {
            while (!checkBottomCollision(active))
            {
                active.step(VecDirection.Down);
            }

        }

        public void parseKeyUp(string input)
        {
            if (input.Equals("ArrowDown") || input.Equals("s"))
            {
                holdingDown = false;
                snapDownToken.Cancel();
            }
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

        public void updateGameState(Score s)
        {
            // TODO  - build UI, constraint playing field to 80%
            // TODO show score string, show next blocks

            if (checkBottomCollision(active) is false)
                active.step(VecDirection.Down);
            else
                settleActive();

            s.scoreValue += searchLines();
            // move blocks down
            // settle block on bottom
            // check for lines
            // score
            // spawn a new block
        }

        private void settleActive()
        {
            active.parts.ForEach(p => field[(int)p.pos.Y][(int)p.pos.X] = p);
            active = next.Dequeue();
            active.offsetHorzPos(activeEdges + 4);
            next.Enqueue(getRandomTetromino());
            snapDownToken = new CancellationTokenSource();
        }

        private int searchLines()
        {
            int lineCount = 0;
            for (int i = field.Length - 1; i >= 0; i--)
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

                if (lineFormed)
                {
                    lineCount++;
                    int k = i;
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
            baseScore += lineCount;
            return baseScore * lineCount;
        }
    }
}