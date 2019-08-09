using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;

public class Player : Unit
{
    public bool inLight;

    private void Start()
    {
        InputManager.INSTANCE.DirectionInput += MoveAlongDirection;
        pos.x = ruleSet.GRID_WIDTH / 2;
        pos.y = ruleSet.GRID_HEIGHT / 2;
        this.transform.position = new Vector3(pos.x, 0, pos.y);
    }

    private void OnDisable()
    {
        InputManager.INSTANCE.DirectionInput -= MoveAlongDirection;
    }

    private void Update()
    {
        if (inLight)
        {

        }
        else
        {

        }
    }
}
