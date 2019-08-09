using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;

public class GameManager : MonoBehaviour
{
    public delegate void GameStateDelegate(HealthSystem.Entity entity);
    public GameStateDelegate gameOverDelegate;

    public enum GameState { play, pause };

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
        ServiceLocator.SetDebugProvider(new RealDebugProvider());
        GridHolder.GenerateGrid();
        WorldPainter.PaintWorld();
        Pathfinding.SwitchToManhattan();
    }

    public void EvaluateGameState(HealthSystem.Entity entity)
    {
        switch (entity)
        {
            case HealthSystem.Entity.Player:
                //lose the game
                break;
            case HealthSystem.Entity.Monster:
                //win the game
                break;
            default:
                break;
        }
    }
}
