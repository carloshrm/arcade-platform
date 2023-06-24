using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace cmArcade.Shared.Asteroids;

public class Asteroid
{
    public Vector2 pos { get; set; }
    public int healthPoints { get; set; }

    public Asteroid((int row, int col) initialPos, int hp = 1)
    {
        pos = new Vector2(initialPos.col, initialPos.row);
        healthPoints = hp;
    }
}

public class AsteroidsField : IGameField
{
    private readonly (int row, int col) limits;
    private readonly Random rng = new Random();

    public int activeEdges { get; private set; }
    public string uiMessage { get; set; } = string.Empty;
    public int scoreMult { get; set; } = 1;

    private int difficulty = 1;
    private readonly int asteroidCount = 8;
    private readonly int baseScore = 3;

    private PlayerShip player { get; set; }
    private List<Asteroid> asteroids { get; set; }

    public AsteroidsField((int row, int col) limits, int difficulty)
    {
        player = new PlayerShip((limits.row / 2, limits.col / 2));
        this.difficulty = difficulty;

        // spawn randomized asteroid field

        // add float movement

    }

    private List<Asteroid> GenerateField()
    {
        var field = new List<Asteroid>();
        int posOffset = (int)Math.Ceiling(limits.col * 0.05);
        int posScale = limits.col / 10;
        for (int i = 1; i <= asteroidCount; i++)
        {
            field.Add(new Asteroid((posScale * i));
        }
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

    public void updateGameState(Score s)
    {
        
        // ship movement, spin
        // shots
        // hit detection
        // respawn asteroids

    }
}
