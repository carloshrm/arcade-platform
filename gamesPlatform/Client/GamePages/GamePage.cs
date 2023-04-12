
using System.Timers;

using cmArcade.Shared;

using Microsoft.AspNetCore.Components;

using Timer = System.Timers.Timer;

namespace cmArcade.Client.GamePages
{
    public abstract class GamePage<T> : ComponentBase, IDisposable
        where T : IGameField
    {
        protected readonly AppID _myID;
        protected T? game { get; set; }

        public Score currentScore { get; set; }
        public Score highScore { get; set; }
        protected Timer gameControl { get; set; }
        protected Timer canvasRefresh { get; set; }

        public GamePage(AppID app)
        {
            _myID = app;
            canvasRefresh = new Timer(1000 / 30) { AutoReset = true, Enabled = false };
            gameControl = new Timer(50) { AutoReset = true, Enabled = false };
            currentScore = new Score(_myID);
            highScore = currentScore;
            game = default;
        }

        protected void toggleControlObjects()
        {
            gameControl.Enabled = !gameControl.Enabled;
            canvasRefresh.Enabled = !canvasRefresh.Enabled;
        }

        protected abstract Task startGame();
        protected abstract Task stopGame();
        protected abstract Task resetGame();
        protected abstract void runGame(Object? o, ElapsedEventArgs e);
        protected abstract void drawGame(Object? o, ElapsedEventArgs e);
        public void Dispose()
        {
            gameControl.Dispose();
            canvasRefresh.Dispose();
            DisposeHarder();
        }
        public abstract void DisposeHarder();
    }
}
