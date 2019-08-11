using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;

public class InputManager : MonoBehaviour
{
    public static InputManager INSTANCE;
    public float moveWaitTimer;
    public float baseSpeed;


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

    private void Start()
    {
        baseSpeed = ruleSet.PLAYER_MOVEMENT_TICK;
    }

    public enum Direction { Up, Down, Right, Left, NONE };

    public delegate void DirectionalInput(Direction dir, float _speedValue);
    public DirectionalInput DirectionInput;

    private void Update()
    {
        if (GameManager.INSTANCE.GetGameState() == GameManager.GameState.play)
        {
            MoveInput();
        }
        else
        {
            PauseInput();
        }
    }

    private void MoveInput()
    {
        if (!Player.isBurning)
        {
            moveWaitTimer -= Time.deltaTime;
        }
        else
        {
            moveWaitTimer -= Time.deltaTime * ruleSet.PLAYER_BURNING_EXTRA_SPEED_PERCENTAGE;
        }
        if (moveWaitTimer > 0)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveWaitTimer = baseSpeed;
            DirectionInput?.Invoke(Direction.Up, 1);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveWaitTimer = baseSpeed;
            DirectionInput?.Invoke(Direction.Down, 1);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveWaitTimer = baseSpeed;
            DirectionInput?.Invoke(Direction.Left, 1);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveWaitTimer = baseSpeed;
            DirectionInput?.Invoke(Direction.Right, 1);
        }

        if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Escape))
        {
            GameManager.INSTANCE.SetGameState(GameManager.GameState.pause);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameManager.INSTANCE.NewWorld();
        }
    }

    private void PauseInput()
    {
        if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Escape))
        {
            GameManager.INSTANCE.SetGameState(GameManager.GameState.play);
        }
    }
}
