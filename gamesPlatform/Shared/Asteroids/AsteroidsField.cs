﻿using System.Numerics;

using static System.Net.Mime.MediaTypeNames;

namespace cmArcade.Shared.Asteroids;

public class AsteroidsField : IGameField
{
    private readonly (float row, float col) limits;
    public int activeEdges { get; private set; }
    public string uiMessage { get; set; } = string.Empty;
    public int scoreMult { get; set; } = 1;
    private readonly int asteroidLimit = 5;
    private readonly int baseScore = 3;
    private PlayerShip player { get; set; }
    public List<Asteroid> asteroids { get; set; }

    public AsteroidsField((float row, float col) limits)
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
            Asteroid newAsteroid;
            do
            {
                float nextX = Random.Shared.Next(0, (int)limits.col);
                float nextY = Random.Shared.Next(0, (int)limits.row);
                newAsteroid = new Asteroid(new Vector2(nextX, nextY));
            } while (player.GetParts().Any(p => CheckCollision(newAsteroid, p))
                    || field.Any(a => CheckCollision(newAsteroid, a)));
            field.Add(newAsteroid);
        }
        return field;
    }
    private Asteroid SpawnAsteroidOutside()
    {
        var hor = Random.Shared.Next(-1, 2);
        var ver = Random.Shared.Next(-1, 2);
        return new Asteroid(new Vector2(limits.col * hor, limits.row * ver));
    }

    private bool CheckCollision(ISimpleVectorialObject target, float objX, float objY)
    {
        return target.pos.Y + target.model.topRightBounds.Y >= objY
            && target.pos.Y + target.model.bottomLeftBounds.Y <= objY
            && target.pos.X + target.model.bottomLeftBounds.X <= objX
            && target.pos.X + target.model.topRightBounds.X >= objX;
    }

    private bool CheckCollision(ISimpleVectorialObject target, ISimpleVectorialObject obj)
    {
        return CheckCollision(target, obj.pos.X, obj.pos.Y);
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
                }
            }
        }
    }

    private void BumpAsteroids()
    {
        foreach (var currentAst in asteroids.Where(c => !c.isPrimary))
        {
            foreach (var floatingAst in asteroids)
            {
                if (currentAst != floatingAst
                    && Math.Abs(currentAst.pos.X - floatingAst.pos.X) <= 100
                    && Math.Abs(currentAst.pos.Y - floatingAst.pos.Y) <= 100)
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
                    var fragmentB = new Asteroid(new Vector2(a.pos.X + fragmentA.model.objWidth, a.pos.Y + fragmentA.model.objHeight), false);
                    fragmentA.SetNormalizedFloatDir(a.floatDir);
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
        return score;
    }

    public bool CheckGameOver()
    {
        var playerParts = player.GetParts().Take(2);
        return player.healthPoints == 0 
            || asteroids.Where(a => a.canColide)
                        .Any(a => CheckCollision(a, playerParts.First())
                            || CheckCollision(a, playerParts.Last()));
    }

    public object GetPlayer()
    {
        return player;
    }

    public void ShowFieldMessage(string msg)
    {
        // TODO
    }

    public void SetScoreMultiplier(int val)
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

    private void RespawnAsteroids()
    {
        int ct = asteroids.Count(a => a.isPrimary);
        while (ct++ < asteroidLimit)
        {
            asteroids.Add(SpawnAsteroidOutside());
        }
    }

    public void UpdateGameState(Score s)
    {
        player.UpdatePosition(limits);
        CheckHit();
        player.UpdateShots(limits);
        s.scoreValue += UpdateFieldState();
        RespawnAsteroids();
        asteroids.ForEach(a => a.UpdatePosition(limits));
        BumpAsteroids();
    }
}
