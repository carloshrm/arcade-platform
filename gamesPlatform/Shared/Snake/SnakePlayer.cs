using System.Numerics;

namespace cmArcade.Shared
{
    public class SnakePlayer : ISimpleGameObject
    {
        internal class SnakeModel : CanvasRenderedAsset
        {
            public override string color { get; init; }
            public override float width { get; init; }
            public override float height { get; init; }

            public static readonly SnakeModel snakeModel = new SnakeModel { color = "darkgreen", width = 1, height = 1 };
        }

        public int healthPoints { get; set; }
        public Vector2 pos { get; set; }
        public List<TailPiece> tail { get; set; }
        public Vector2 movingDirection { get; set; }
        public CanvasRenderedAsset model { get; set; } = SnakeModel.snakeModel;


        public SnakePlayer(int startingSize, (int r, int c) boardLimits)
        {
            healthPoints = startingSize;
            tail = new List<TailPiece>();
            pos = new Vector2(boardLimits.c / 2, boardLimits.r / 2);
            movingDirection = VecDirection.Left;
        }

        public void growSnake(Object? sender, EventArgs e)
        {
            healthPoints++;
        }

        public void parseMoveCommand(string keyValue)
        {
            switch (keyValue)
            {
                case "ArrowUp":
                case "w":
                    if (movingDirection != VecDirection.Down)
                        movingDirection = VecDirection.Up;
                    break;
                case "ArrowDown":
                case "s":
                    if (movingDirection != VecDirection.Up)
                        movingDirection = VecDirection.Down;
                    break;
                case "ArrowLeft":
                case "a":
                    if (movingDirection != VecDirection.Right)
                        movingDirection = VecDirection.Left;
                    break;
                case "ArrowRight":
                case "d":
                    if (movingDirection != VecDirection.Left)
                        movingDirection = VecDirection.Right;
                    break;
                default:
                    break;
            }
        }

        public bool updatePosition((int row, int col) limits)
        {
            pos += movingDirection;
            return true;
        }
    }
}
