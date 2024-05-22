using System.Numerics;
using System.Runtime.CompilerServices;

namespace cmArcade.Shared.Pac;

public class PacField : IGameField
{
    private readonly (float row, float col) limits;
    public string uiMessage { get; set; } = string.Empty;
    public int scoreMult { get; set; } = 1;
    private int activeDogs = 1;

    public PacNyan player { get; set; }
    public PacMaze maze {  get; set; }

    public PacField((float row, float col) limits)
    {
        this.limits = limits;
        player = new PacNyan((int)limits.col / 2, (int)limits.row / 2);
        maze = new PacMaze();
    }

    public void parseKeyDown(string input)
    {
        Vector2? nextDir = input switch
        {
            "ArrowUp" or "w" => VecDirection.Up,
            "ArrowDown" or "s" => VecDirection.Down,
            "ArrowLeft" or "a" => VecDirection.Left,
            "ArrowRight" or "d" => VecDirection.Right,
            _ => null,
        };
        if (nextDir.HasValue)
            player.SetDirection(nextDir.Value);
    }

    public void parseKeyUp(string dir)
    {
        return;
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
        //
    }

    public void ShowFieldMessage(string msg)
    {
        throw new NotImplementedException();
    }

    public void UpdateGameState(Score s)
    {
        var nextPos = player.pos + player.movingDirection;
        if (player.pos.X == 0 || player.pos.Y == 0 || player.pos.X == limits.col || player.pos.Y == limits.row || maze.collisionMap[(int)nextPos.X][(int)nextPos.Y])
            player.SetDirection(VecDirection.Zero);
        else
            player.UpdatePosition(limits);

    }
}
