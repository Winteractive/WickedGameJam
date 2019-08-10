using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;

[RequireComponent(typeof(InLightChecker))]
public class Player : Unit
{
    public bool inLight;
    public List<string> stepSFXList;
    Vector3 startPosition;
    InLightChecker lightChecker;


    private void Start()
    {
        lightChecker = GetComponent<InLightChecker>();
        InputManager.INSTANCE.DirectionInput += MoveAlongDirection;
        pos.x = ruleSet.GRID_WIDTH / 2;
        pos.y = ruleSet.GRID_HEIGHT / 2;
        if (GameManager.INSTANCE.GetGameState() == GameManager.GameState.gameFinish)
        {
            //new pos
            this.transform.position = startPosition;
        }
        else
        {
            //new pos
            this.transform.position = new Vector3(pos.x, 0, pos.y);
        }
        hp = new Health();
        hp.myUnit = this;
        hp.SetMaxHealth(ruleSet.PLAYER_HEALTH);
        hp.SetCurrentHealth(ruleSet.PLAYER_HEALTH);
        hp.IsDead += GameManager.INSTANCE.GameOver;
        hp.IsDead += SavePlayerPos;
        GameManager.INSTANCE.NewWorldCreated += RefreshForNewWorld;
    }



    private void OnDisable()
    {
        InputManager.INSTANCE.DirectionInput -= MoveAlongDirection;
        hp.IsDead -= GameManager.INSTANCE.GameOver;
        hp.IsDead -= SavePlayerPos;
        GameManager.INSTANCE.NewWorldCreated -= RefreshForNewWorld;


    }


    public override void MoveAlongDirection(InputManager.Direction direction)
    {
        base.MoveAlongDirection(direction);
        if (stepSFXList == null || stepSFXList.Count == 0)
        {
            return;
        }
        ServiceLocator.GetAudioProvider().PlaySoundEvent(stepSFXList.GetRandom());
    }

    private void Update()
    {
        if (GameManager.INSTANCE.GetGameState() == GameManager.GameState.pause)
        {
            return;
        }

        if (!lightChecker.inLight)
        {
            hp.TakeDamage(ruleSet.PLAYER_HEALTH_LOSS_RATE * Time.deltaTime);
        }
        else
        {
            hp.GainHealth(ruleSet.PLAYER_HEALTH_GAIN_RATE * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            hp.TakeDamage(ruleSet.PLAYER_HEALTH_LOSS_RATE);
        }
    }

    public void SavePlayerPos()
    {
        startPosition = this.transform.position;
    }
}
