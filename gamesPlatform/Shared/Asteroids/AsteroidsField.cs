using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

    private readonly int asteroidLimit = 5;
    private readonly int baseScore = 3;

    private PlayerShip player { get; set; }
    public List<Asteroid> asteroids { get; set; }

    public AsteroidsField((int row, int col) limits)
    {
        this.limits = limits;
        player = new PlayerShip((limits.row / 2, limits.col / 2));
        asteroids = GenerateField();
        // spawn randomized asteroid field
        // add float movement
    }

    private List<Asteroid> GenerateField()
    {
        var field = new List<Asteroid>();
        var playerPos = player.getParts().Select(p => p.pos);

        int astCount = asteroidLimit;
        int initialX = limits.col / asteroidLimit;
        int initialY = limits.row / asteroidLimit;
        var prevPoint = new Vector2(initialX, initialY);
        while (astCount-- > 0)
        {
            field.Add(new Asteroid(prevPoint));

            float nextX = 0;
            float nextY = 0;
            do
            {
                nextX = (limits.col / 2) + rng.Next(-initialX, initialX);
                nextY = prevPoint.Y + initialY + rng.Next(0, 50);
            } while (playerPos.Any(p => p.X == nextX || p.Y == nextY));

            prevPoint = new Vector2(nextX, nextY);
        }
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
            case "Space":
            case " ":
                player.Fire();
                break;
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
                player.Rotate(true);
                break;
            default:
                break;
        }
    }

    public void parseKeyUp(string dir)
    {
        switch (dir)
        {
            case "ArrowLeft":
            case "a":
            case "ArrowRight":
            case "d":
                player.StopRotation();
                break;
            default:
                break;
        }
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
