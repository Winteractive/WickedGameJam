using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;
using static InputManager;
public class Monster : Unit
{
    public enum Modes { Search, Hunt };
    public Modes currentMode;
    float moveTimer;
    Unit target;
    float speed;

    void Start()
    {
        moveTimer = 3f;
        speed = ruleSet.MONSTER_MOVEMENT_TICK;
        target = GameObject.FindWithTag("Player").GetComponent<Unit>();
        pos.x = 5;
        pos.y = 5;
        this.transform.position = new Vector3(5, 0, 5);
        hp.SetMaxHealth(ruleSet.MONSTER_HEALTH);
        hp.SetCurrentHealth(ruleSet.MONSTER_HEALTH);
    }


    void Update()
    {
        switch (currentMode)
        {
            case Modes.Search:
                currentMode = Modes.Hunt;
                break;
            case Modes.Hunt:
                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0)
                {
                    moveTimer = speed;
                    Direction direction = Pathfinding.GetNextStepDirection(this, target);
                    if (direction != Direction.NONE)
                    {
                        MoveAlongDirection(direction);
                    }
                }
                break;
            default:
                break;
        }

    }
}
