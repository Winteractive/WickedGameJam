using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;
using static InputManager;
using System;

public class Monster : Unit
{
    public enum Modes { Search, Hunt, Happy, Dead };
    public Modes currentMode;

    MonsterLight light;

    Vector3 startPosition;
    float moveTimer;
    Unit target;
    float actionWait;
    float timeBetweenSteps;

    float happyTimer;
    float monsterGrowth;

    float newSearchPointTimer;
    Cell searchCell;

    public LayerMask searchMask;
    RaycastHit hit;

    protected override void RefreshForNewWorld()
    {
        base.RefreshForNewWorld();
        moveTimer = 3;
        hp = new Health();
        hp.SetMaxHealth(ruleSet.MONSTER_HEALTH + monsterGrowth);
        hp.SetCurrentHealth(ruleSet.MONSTER_HEALTH + monsterGrowth);
    }

    void Start()
    {
        light = GetComponentInChildren<MonsterLight>();
        pos.x = 5;
        pos.y = 5;
        if (GameManager.INSTANCE.GetGameState() == GameManager.GameState.gameFinish)
        {
            //player previous pos
            monsterGrowth = ruleSet.MONSTER_GROWTH_FACTOR;
            this.transform.position = startPosition;
        }
        else
        {
            //new pos
            monsterGrowth = 0;
            this.transform.position = new Vector3(5, 0, 5);
        }

        moveTimer = 3f;
        target = GameObject.FindWithTag("Player").GetComponent<Unit>();
        actionWait = ruleSet.MONSTER_MOVEMENT_TICK_SEARCH - monsterGrowth;
        hp = new Health();
        hp.SetMaxHealth(ruleSet.MONSTER_HEALTH + monsterGrowth);
        hp.SetCurrentHealth(ruleSet.MONSTER_HEALTH + monsterGrowth);

        hp.IsDead += GameManager.INSTANCE.GameFinished;
        hp.IsDead += GetPlayerPosition;
        GameManager.INSTANCE.NewWorldCreated += RefreshForNewWorld;

        ChangeMode(Modes.Search);
    }

    private void OnDisable()
    {
        hp.IsDead -= GameManager.INSTANCE.GameFinished;
        hp.IsDead -= GetPlayerPosition;
        GameManager.INSTANCE.NewWorldCreated -= RefreshForNewWorld;

    }

    private void ChangeMode(Modes newMode)
    {
        currentMode = newMode;
        switch (currentMode)
        {
            case Modes.Search:
                newSearchPointTimer = ruleSet.MONSTER_SEARCH_TIMER.GetRandomValue();
                searchCell = GridHolder.GetRandomWalkableCell();
                timeBetweenSteps = ruleSet.MONSTER_MOVEMENT_TICK_SEARCH;
                break;
            case Modes.Hunt:
                timeBetweenSteps = ruleSet.MONSTER_MOVEMENT_TICK_HUNT;
                break;
            case Modes.Happy:
                timeBetweenSteps = int.MaxValue;
                happyTimer = ruleSet.MONSTER_WAIT_TIME;
                break;
            case Modes.Dead:
                timeBetweenSteps = int.MaxValue;
                break;
        }
    }

    void Update()
    {
        if (GameManager.INSTANCE.GetGameState() == GameManager.GameState.pause)
        {
            return;
        }
        if (GameManager.INSTANCE.GetGameState() == GameManager.GameState.gameOver)
        {
            return;
        }
        if (GameManager.INSTANCE.GetGameState() == GameManager.GameState.gameFinish)
        {
            return;
        }

        if (currentMode == Modes.Dead)
        {
            return;
        }

        light.UpdateColor(hp.GetCurrentHealth(), hp.GetMaxHealth());

        hp.TakeDamage(ruleSet.MONSTER_HEALTH_LOSS_RATE * Time.deltaTime);


        if (hp.GetCurrentHealth() <= 0 && currentMode != Modes.Dead)
        {
            ChangeMode(Modes.Dead);
        }

        switch (currentMode)
        {
            case Modes.Search:


                if (LookForPlayer())
                {
                    ChangeMode(Modes.Hunt);
                }

                newSearchPointTimer -= Time.deltaTime;
                if (newSearchPointTimer <= 0)
                {
                    newSearchPointTimer = ruleSet.MONSTER_SEARCH_TIMER.GetRandomValue();
                    searchCell = GridHolder.GetRandomWalkableCell();
                }

                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0)
                {
                    moveTimer = timeBetweenSteps;
                    Direction direction = Pathfinding.GetNextStepDirection(this.pos.GetAsVector3Int(), searchCell.pos.GetAsVector3Int());
                    if (direction != Direction.NONE)
                    {
                        MoveAlongDirection(direction);
                    }
                }


                break;
            case Modes.Hunt:
                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0)
                {
                    moveTimer = timeBetweenSteps;
                    Direction direction = Pathfinding.GetNextStepDirection(this, target);
                    if (direction != Direction.NONE)
                    {
                        MoveAlongDirection(direction);
                    }
                }
                break;
            case Modes.Happy:
                happyTimer -= Time.deltaTime;
                if (happyTimer <= 0)
                {
                    ChangeMode(Modes.Search);
                }
                break;
            case Modes.Dead:
                break;
            default:
                break;
        }

    }

    private bool LookForPlayer()
    {
        if (Vector3.Distance(transform.position, target.gameObject.transform.position) <= ruleSet.LIGHT_RADIUS)
        {
            ServiceLocator.GetDebugProvider().Log("inside distance");
            if (Physics.Linecast(transform.position, target.transform.position, out hit, searchMask))
            {
                ServiceLocator.GetDebugProvider().Log("linecasted");
                if (hit.collider.gameObject == target.gameObject)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void GetPlayerPosition()
    {
        //Debug.Log("got player position");
        startPosition = target.transform.position;
    }
}
