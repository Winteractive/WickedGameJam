using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;

public class GameManager : MonoBehaviour
{
    public delegate void GameStateDelegate();
    public GameStateDelegate NewWorldCreated;

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

        ServiceLocator.SetDebugProvider(new RealDebugProvider());
        FindObjectOfType<Rules>().SetRules();
        ruleSet = ruleSet.CreateClone();
        GridHolder.GenerateGrid();
        WorldPainter.PaintWorld();
        Pathfinding.SwitchToManhattan();
        SetGameState(GameState.play);
    }

    void Start()
    {

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

    public void NewWorld()
    {
        Pathfinding.SetUp();
        GridHolder.GenerateGrid();
        WorldPainter.RemoveWorld();
        WorldPainter.PaintWorld();
        Pathfinding.SwitchToManhattan();
        NewWorldCreated?.Invoke();
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
