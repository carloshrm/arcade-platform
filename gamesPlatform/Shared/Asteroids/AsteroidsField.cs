using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmArcade.Shared.Asteroids;

public class AsteroidsField : IGameField
{
    private readonly (int row, int col) limits;
    private readonly Random rng = new Random();

    public int activeEdges { get; private set; }
    public string uiMessage { get; set; } = string.Empty;
    public int scoreMult { get; set; } = 1;

    private readonly int asteroidLimit = 8;
    private readonly int baseScore = 3;

    private PlayerShip player { get; set; }
    public List<Asteroid> asteroids { get; set; }

    public AsteroidsField((int row, int col) limits)
    {
        player = new PlayerShip((limits.row / 2, limits.col / 2));
        asteroids = GenerateField();
        // spawn randomized asteroid field
        // add float movement
    }

    private List<Asteroid> GenerateField()
    {
        var field = new List<Asteroid>();

        field.Add(new Asteroid((200, 200)));
        field.Add(new Asteroid((200, 600)));
        field.Add(new Asteroid((600, 200)));
        field.Add(new Asteroid((600, 600)));

        return field;
    }

    public bool checkGameOver()
    {
        return false;
    }

    public object getPlayer()
    {
        return player;
    }

    public void setMessage(string msg)
    {
        // TODO
    }

    public void setScoreMultiplier(int val)
    {
        // TODO
    }

    public void parseKeyDown(string input)
    {
        switch (input)
        {
            case "ArrowUp":
            case "w":
                player.Thrust();
                break;
            case "ArrowDown":
            case "s":
                player.Thrust(false);
                break;
            case "ArrowLeft":
            case "a":
                player.Rotate();
                break;
            case "ArrowRight":
            case "d":
                player.Rotate();
                break;
            case " ":
                break;
            default:
                break;
        }
    }

    public void parseKeyUp(string dir)
    {
        // 
    }

    public void updateGameState(Score s)
    {
        Console.WriteLine("update");
        player.updatePosition(limits);
        // ship movement, spin
        // shots
        // hit detection
        // respawn asteroids

    }
}
