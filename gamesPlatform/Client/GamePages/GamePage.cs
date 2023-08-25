
using System.Timers;

using cmArcade.Shared;

using Microsoft.AspNetCore.Components;

using Timer = System.Timers.Timer;

namespace cmArcade.Client.GamePages;

public abstract class GamePage<T> : ComponentBase, IDisposable
    where T : IGameField
{
    protected readonly AppID _myID;
    protected T? game { get; set; }

    public Score currentScore { get; set; }
    public Score highScore { get; set; }
    protected Timer gameControl { get; set; }
    protected Timer canvasRefresh { get; set; }
    protected (string font, string color) textStyle { get; set; }
    protected List<string> assetList { get; set; }


    public GamePage(AppID app)
    {
        _myID = app;
        textStyle = ("12px \"Press Start 2P\"", "white");
        assetList = new List<string>();
        canvasRefresh = new Timer(1000 / 60) { AutoReset = true, Enabled = false };
        gameControl = new Timer(50) { AutoReset = true, Enabled = false };
        currentScore = new Score(_myID);
        highScore = currentScore;
        game = default;
    }

    protected void ToggleControlObjects()
    {
        gameControl.Enabled = !gameControl.Enabled;
        canvasRefresh.Enabled = !canvasRefresh.Enabled;
    }

    protected abstract Task StartGame();
    protected abstract Task StopGame();
    protected abstract Task ResetGame();
    protected abstract void RunGame(object? o, ElapsedEventArgs e);
    protected abstract void DrawGame(object? o, ElapsedEventArgs e);
    public void Dispose()
    {
        gameControl.Dispose();
        canvasRefresh.Dispose();
        DisposeHarder();
    }
    public abstract void DisposeHarder();
}
