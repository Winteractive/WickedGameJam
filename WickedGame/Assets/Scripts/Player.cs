﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Rules;

[RequireComponent(typeof(InLightChecker))]
public class Player : Unit
{
    public Animator animator;
    public bool inLight;
    public List<string> stepSFXList;
    Vector3 startPosition;
    InLightChecker lightChecker;
    public ParticleSystem fire;
    public Light fireLight;
    public static bool isBurning;

    private Jump jump;

    Slider healthSlider;

    int everyOtherStep;

    Vector3 curPos;
    Vector3 prevPos;
    public float deltaPosDif;

    public float idleTime;

    private void Start()
    {
        jump = GetComponentInChildren<Jump>();
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("Idle");
        prevPos = transform.position;
        curPos = prevPos;
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

        healthSlider = GameObject.FindWithTag("HealthSlider").GetComponent<Slider>();
        healthSlider.maxValue = hp.GetMaxHealth();
        healthSlider.minValue = 0;
        healthSlider.value = hp.GetCurrentHealth();

        hp.HpChanged += UpdateHealthSlider;



        GameManager.INSTANCE.NewWorldCreated += RefreshForNewWorld;
    }



    private void OnDisable()
    {
        InputManager.INSTANCE.DirectionInput -= MoveAlongDirection;
        hp.IsDead -= GameManager.INSTANCE.GameOver;
        hp.IsDead -= SavePlayerPos;
        GameManager.INSTANCE.NewWorldCreated -= RefreshForNewWorld;


    }


    private void UpdateHealthSlider(float healthValue)
    {
        healthSlider.value = healthValue;
    }

    public override void MoveAlongDirection(InputManager.Direction direction, float _speed)
    {
        _speed = ruleSet.PLAYER_MOVEMENT_TICK;
        if (isBurning)
        {
            _speed = _speed / (ruleSet.PLAYER_BURNING_EXTRA_SPEED_PERCENTAGE);
        }
        base.MoveAlongDirection(direction, _speed);


        GridHolder.CheckForBranch(pos.GetAsVector3Int());
        if (stepSFXList == null || stepSFXList.Count == 0)
        {
            ServiceLocator.GetDebugProvider().Log("step sfx is null");
            return;
        }
        if (everyOtherStep == 2)
        {
            everyOtherStep = 0;
            ServiceLocator.GetAudioProvider().PlaySoundEvent(stepSFXList.GetRandom(), volume: 0.6f);
        }
        everyOtherStep++;
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

    private void LateUpdate()
    {
        prevPos = curPos;
        curPos = this.transform.position;
        deltaPosDif = Vector3.Distance(prevPos, curPos);
        if (deltaPosDif == 0)
        {
            idleTime += Time.deltaTime;
        }
        else
        {
            idleTime = 0;
        }

        animator.SetBool("Moving", !(idleTime > 0.15f));

    }

    public void SavePlayerPos()
    {
        startPosition = this.transform.position;
    }

    internal void GetAttacked()
    {
        ServiceLocator.GetDebugProvider().Log("attacked");
        hp.ReduceMaximumHealth(ruleSet.MONSTER_DAMAGE);
        Screenshake.INSTANCE.DoScreenshake();
        // if (hp.GetCurrentHealth() > 0)
        // {
        if (fire)
        {
            if (jump)
            {
                jump.DoJump(1, 1f);
            }
            ServiceLocator.GetAudioProvider().PlaySoundEvent("Burning");
            isBurning = true;
            fireLight.enabled = true;
            fire.Play();
            Invoke("StopFire", 2f);
        }

        if (hp.GetCurrentHealth() <= 0)
        {
            ServiceLocator.GetAudioProvider().PlaySoundEvent("Death");
        }
        else
        {
            ServiceLocator.GetAudioProvider().PlaySoundEvent("TakeHit");
        }
        // }

    }

    private void StopFire()
    {
        isBurning = false;
        fireLight.enabled = false;
        fire.Stop();
    }
}
