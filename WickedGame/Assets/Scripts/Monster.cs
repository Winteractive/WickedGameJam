using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;
using static InputManager;


public class Monster : Unit
{
    public enum Modes { Search, Hunt, Happy, Dead };
    public Modes currentMode;
    float moveTimer;
    Unit target;
    float timeBetweenSteps;

    float happyTimer;

    void Start()
    {
        moveTimer = 3f;

        target = GameObject.FindWithTag("Player").GetComponent<Unit>();
        pos.x = 5;
        pos.y = 5;
        this.transform.position = new Vector3(5, 0, 5);
        hp = new Health();
        hp.SetMaxHealth(ruleSet.MONSTER_HEALTH);
        hp.SetCurrentHealth(ruleSet.MONSTER_HEALTH);
        hp.IsDead += GameManager.INSTANCE.GameFinished;
        ChangeMode(Modes.Search);
    }

    private void ChangeMode(Modes newMode)
    {
        currentMode = newMode;
        switch (currentMode)
        {
            case Modes.Search:
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

        hp.TakeDamage(ruleSet.MONSTER_HEALTH_LOSS_RATE * Time.deltaTime);
        if (hp.GetCurrentHealth() == 0 && currentMode != Modes.Dead)
        {
            ChangeMode(Modes.Dead);
        }

        switch (currentMode)
        {
            case Modes.Search:
                currentMode = Modes.Hunt;
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
}
