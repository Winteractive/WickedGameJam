using System.Collections;
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
        SetGameState(GameState.play);
    }

    void Start()
    {
        ServiceLocator.SetDebugProvider(new RealDebugProvider());
        ruleSet = ruleSet.CreateClone();
        GridHolder.GenerateGrid();
        WorldPainter.PaintWorld();
        Pathfinding.SwitchToManhattan();
        SetGameState(GameState.play);
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.play:
                Time.timeScale = 1;
                break;
            case GameState.pause:
                Time.timeScale = 0;
                break;
            case GameState.gameOver:
                Time.timeScale = 0;
                break;
            case GameState.gameFinish:
                Time.timeScale = 0;
                break;
            default:
                break;
        }
    }

    void NewWorld()
    {
        GridHolder.GenerateGrid();
        WorldPainter.RemoveWorld();
        WorldPainter.PaintWorld();
        Pathfinding.SetUp();
        Pathfinding.SwitchToManhattan();
        SetGameState(GameState.play);
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    IEnumerator ResetDelay()
    {
        yield return new WaitForSecondsRealtime(ruleSet.RESET_DELAY);

        NewWorld();
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
        ServiceLocator.GetDebugProvider().Log("You survived. Congratulations!");
    }
}
