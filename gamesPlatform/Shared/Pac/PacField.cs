using System.Runtime.CompilerServices;

namespace cmArcade.Shared.Pac;

public class PacField : IGameField
{
    private readonly (float row, float col) limits;    
    public string uiMessage { get; set; } = string.Empty;
    public int scoreMult { get; set; } = 1;
    private int activeDogs = 1;

    public PacNyan player { get; set; }

    public PacField((float row, float col) limits)
    {
        this.limits = limits;
    }

    public void parseKeyDown(string input)
    {
        switch (input)
        {
            case "ArrowUp":
            case "w":
                //player.Thrust();
                break;
            case "ArrowDown":
            case "s":
                //player.Thrust(fw: false);
                break;
            case "ArrowLeft":
            case "a":
                //player.Rotate(cw: false);
                break;
            case "ArrowRight":
            case "d":
                //player.Rotate(cw: true);
                break;
            default:
                break;
        }
    }

    public void parseKeyUp(string dir)
    {
        switch (dir)
        {
            case "ArrowUp":
            case "w":
                break;
            case "ArrowDown":
            case "s":
                break;
            case "ArrowLeft":
            case "a":
                break;
            case "ArrowRight":
            case "d":
                break;
            default:
                break;
        }
    }

    public bool CheckGameOver()
    {
        return false;
    }

    public object GetPlayer()
    {
        return player;
    }

    public void SetScoreMultiplier(int val)
    {
        
    }

    public void ShowFieldMessage(string msg)
    {
        throw new NotImplementedException();
    }

    public void UpdateGameState(Score s)
    {
        player.walk(limits);
    }
}
