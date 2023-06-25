﻿using System.Numerics;

namespace cmArcade.Shared.Asteroids;

public class PlayerShip : IGameObject
{
    public Vector2 pos { get; set; }
    public Vector2 facing { get; set; }
    public Vector2 movement { get; set; }

    public int healthPoints { get; set; }

    public GraphicAsset model { get; set; }
    public List<GraphicAsset>? decals { get; set; }
    public int spriteSelect { get; set; } = 1;

    public PlayerShip((int row, int col) initialPos)
    {
        pos = new Vector2(initialPos.col, initialPos.row);
        healthPoints = 3;
        model = ShipModel.player; 
    }

    public void RotateLeft()
    {

    }

    public bool updatePosition((int row, int col) limits)
    {
        pos += movement;
        return true;
    }
}
