﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager INSTANCE;
    public float moveWaitTimer;


    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            if (INSTANCE != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public enum Direction { Up, Down, Right, Left, NONE };

    public delegate void DirectionalInput(Direction dir);
    public DirectionalInput DirectionInput;

    private void Update()
    {
        NewMethod();
    }

    private void NewMethod()
    {
        moveWaitTimer -= Time.deltaTime;
        if (moveWaitTimer > 0)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveWaitTimer = Rules.ruleSet.PLAYER_MOVEMENT_TICK;
            DirectionInput?.Invoke(Direction.Up);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveWaitTimer = Rules.ruleSet.PLAYER_MOVEMENT_TICK;
            DirectionInput?.Invoke(Direction.Down);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveWaitTimer = Rules.ruleSet.PLAYER_MOVEMENT_TICK;
            DirectionInput?.Invoke(Direction.Left);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveWaitTimer = Rules.ruleSet.PLAYER_MOVEMENT_TICK;
            DirectionInput?.Invoke(Direction.Right);
        }
    }
}