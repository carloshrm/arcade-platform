using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

namespace cmArcade.Shared.Asteroids;

public class AsteroidsField : IGameField
{
    private readonly (int row, int col) limits;
    public int activeEdges { get; private set; }
    public string uiMessage { get; set; } = string.Empty;
    public int scoreMult { get; set; } = 1;
    private readonly int asteroidLimit = 6;
    private readonly int baseScore = 3;
    private PlayerShip player { get; set; }
    public List<Asteroid> asteroids { get; set; }

    public AsteroidsField((int row, int col) limits)
    {
        this.limits = limits;
        player = new PlayerShip((limits.row / 2, limits.col / 2));
        asteroids = GenerateField();
    }

    private List<Asteroid> GenerateField()
    {
        var field = new List<Asteroid>();

        int astCount = asteroidLimit;
        while (astCount-- > 0)
        {
            float nextX = 0;
            float nextY = 0;
            do
            {
                nextX = Random.Shared.Next(100, limits.col - 100);
                nextY = Random.Shared.Next(100, limits.row - 100);
            } while (player.getParts().Any(p => CheckCollision(p, nextX, nextY))
                    || field.Any(a => CheckCollision(a, nextX, nextY)));

            field.Add(new Asteroid(new Vector2(nextX, nextY)));
        }
        return field;
    }

    private Asteroid SpawnAsteroidOutside()
    {
        return new Asteroid(
                new Vector2(
                    Random.Shared.Next(-10, 0),
                    Random.Shared.Next(-10, 0))
                );
    }

    private bool CheckCollision(ISimpleVectorialObject a, float x, float y)
    {
        return a.pos.X + a.model.bottomLeftBounds.X <= x 
            && a.pos.X + a.model.upRightBounds.X >= x
            && a.pos.Y + a.model.upRightBounds.Y <= y 
            && a.pos.Y + a.model.bottomLeftBounds.Y <= y;
    }

    private void CheckHit()
    {
        foreach (var shot in player.shots)
        {
            foreach (var astr in asteroids)
            {
                if (Math.Abs(shot.pos.X - astr.pos.X) > 70 || Math.Abs(shot.pos.Y - astr.pos.Y) > 70)
                    continue;

                Vector2 closestPoint = astr.FindClosestPoint(shot.pos);
                if (Vector2.Distance(shot.pos, astr.pos) <= Vector2.Distance(closestPoint, astr.pos))
                {
                    astr.wasHit = true;
                    astr.SetNormalizedFloatDir(shot.dir + astr.floatDir);
                    shot.fade = true;
                    break;
                }
            }
        }
    }

    private void BumpAsteroids()
    {
        foreach (Asteroid currentAst in asteroids.Where(c => !c.isPrimary))
        {
            foreach (var floatingAst in asteroids)
            {
                if (currentAst != floatingAst
                    && (Math.Abs(currentAst.pos.X - floatingAst.pos.X) <= 100
                        || Math.Abs(currentAst.pos.Y - floatingAst.pos.Y) <= 100))
                {

                    var innerClosestPoint = currentAst.FindClosestPoint(floatingAst.pos);
                    var outerClosestPoint = floatingAst.FindClosestPoint(currentAst.pos);
                    var outer = Vector2.Distance(outerClosestPoint, currentAst.pos);
                    var inner = Vector2.Distance(innerClosestPoint, currentAst.pos);
                    if (outer < inner)
                    {
                        currentAst.Bump(floatingAst.floatDir);
                    }
                }
            }
        }
    }

    private int UpdateFieldState()
    {
        int score = 0;
        var secondary = new List<Asteroid>();
        int removedCount = asteroids.RemoveAll(a =>
        {
            if (a.wasHit)
            {
                score += baseScore;
                if (a.isPrimary)
                {
                    var fragmentA = new Asteroid(new Vector2(a.pos.X, a.pos.Y), false);
                    fragmentA.SetNormalizedFloatDir(a.floatDir);
                    var fragmentB = new Asteroid(new Vector2(a.pos.X + fragmentA.model.objWidth, a.pos.Y + fragmentA.model.objHeight), false);
                    fragmentB.SetNormalizedFloatDir(Vector2.Negate(fragmentA.floatDir));
                    secondary.Add(fragmentA);
                    secondary.Add(fragmentB);
                }
                return true;
            }
            else
                return false;
        });
        asteroids.AddRange(secondary);
        for (int i = 0; i < removedCount; i++)
            asteroids.Add(SpawnAsteroidOutside());
        return score;
    }

    public bool checkGameOver()
    {
        return player.healthPoints == 0;
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
                player.Thrust(fw: false);
                break;
            case "ArrowLeft":
            case "a":
                player.Rotate(cw: false);
                break;
            case "ArrowRight":
            case "d":
                player.Rotate(cw: true);
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
            case "ArrowDown":
            case "s":
                player.StopThrust();
                break;
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
        player.updatePosition(limits);
        CheckHit();
        player.UpdateShots(limits.col, limits.row);
        s.scoreValue += UpdateFieldState();
        asteroids.ForEach((ast) => ast.UpdatePosition(limits.col, limits.row));
        BumpAsteroids();
    }
}
