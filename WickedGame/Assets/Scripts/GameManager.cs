﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;

public class GameManager : MonoBehaviour
{
    public delegate void GameStateDelegate();
    public GameStateDelegate gameOverDelegate;

    public enum GameState { play, pause, gameOver, gameFinish };
    private GameState gameState;

    public static GameManager INSTANCE;

    public void Awake()
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

    void Start()
    {
        if (gameState == GameState.gameFinish)
        {
            //monster grow stronger values
        }
        else if (gameState == GameState.gameOver)
        {
            //no changes to values
        }
        ServiceLocator.SetDebugProvider(new RealDebugProvider());
        ruleSet = ruleSet.CreateClone();
        GridHolder.GenerateGrid();
        WorldPainter.PaintWorld();
        Pathfinding.SwitchToManhattan();
        SetGameState(GameState.play);
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = GameState.play;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        //you burn
        //new level
        //reset monster values
        //new playerSpawn
        //new monsterSpawn
        Debug.Log("You died. Game over!");
    }

    public void GameFinished()
    {
        SetGameState(GameState.gameFinish);
        //monster burn
        //new level
        //improve monster values
        //new playerSpawn
        //monsterSpawn on last death position
        Debug.Log("You survived. Congratulations!");
    }
}
