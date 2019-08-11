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

        ServiceLocator.SetDebugProvider(new NullDebugProvider());
        ServiceLocator.SetAudioProvider(new RealAudioProvider());
        ServiceLocator.GetAudioProvider().PlaySoundEvent("Ambience", true);
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
                // Time.timeScale = 1;
                break;
            case GameState.pause:
                //  Time.timeScale = 0;
                break;
            case GameState.gameOver:
                // Time.timeScale = 0;
                break;
            case GameState.gameFinish:
                // Time.timeScale = 0;
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
        FindObjectOfType<FollowCam>().enabled = true;
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



    public void GameOver()
    {
        Debug.Log("GAME OVER!!");
        if (GetGameState() == GameState.gameOver)
        {
            return;
        }
        SetGameState(GameState.gameOver);
        ServiceLocator.GetAudioProvider().PlaySoundEvent("Death");
        ServiceLocator.GetAudioProvider().PlaySoundEvent("Burning02");
        Player p = FindObjectOfType<Player>();

        if (p.GetComponent<iTween>())
        {
            ServiceLocator.GetDebugProvider().Log("remove itween");
            Destroy(p.GetComponent<iTween>());
        }


        Instantiate(Resources.Load<GameObject>("Prefabs/FirePoof/FirePoof"), p.transform.position + (Vector3.up), Quaternion.identity);
        FindObjectOfType<FollowCam>().enabled = false;
        p.transform.position = Vector3.one * 5000;
        Invoke("NewWorld", 2.5f);
        Debug.Log("You died. Game over!");
    }

    public void GameFinished()
    {
        if (gameState == GameState.gameFinish)
        {
            return;
        }
        SetGameState(GameState.gameFinish);
        ServiceLocator.GetAudioProvider().PlaySoundEvent("Yeah Win");
        Invoke("NewWorld", 2.5f);
        //monster burn
        //new level
        //improve monster values
        //new playerSpawn
        //monsterSpawn on last death position
        ServiceLocator.GetDebugProvider().Log("You survived. Congratulations!");
    }


}
