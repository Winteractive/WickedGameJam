using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;
using static InputManager;
public class Monster : Unit
{

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
    }


    void Update()
    {
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
    }
}
