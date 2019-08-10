﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;

public class Player : Unit
{
    public bool inLight;

    private void Start()
    {
        InputManager.INSTANCE.DirectionInput += MoveAlongDirection;
        pos.x = ruleSet.GRID_WIDTH / 2;
        pos.y = ruleSet.GRID_HEIGHT / 2;
        this.transform.position = new Vector3(pos.x, 0, pos.y);
        hp.myUnit = this;
        hp.SetMaxHealth(ruleSet.PLAYER_HEALTH);
        hp.SetCurrentHealth(ruleSet.PLAYER_HEALTH);
    }

    private void OnDisable()
    {
        InputManager.INSTANCE.DirectionInput -= MoveAlongDirection;
    }

    private void OnTriggerEnter2D(Collider2D monster)
    {
        inLight = true;
    }

    private void OnTriggerExit2D(Collider2D monster)
    {
        inLight = false;
    }

    private void Update()
    {
        if (hp.CheckIfHealthIsZero(hp.GetCurrentHealth()))
        {
            //dead
            GameManager.INSTANCE.EvaluateGameState(this);
        }

        if (inLight)
        {

        }
        else
        {

        }
    }
}
