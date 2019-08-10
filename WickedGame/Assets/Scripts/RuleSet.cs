using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleSet", menuName = "RuleSet", order = 0)]
public class RuleSet : ScriptableObject
{

    public int GRID_WIDTH;
    public int GRID_HEIGHT;

    public Int2 OBSTACLE_AMOUNT;

    public float PLAYER_MOVEMENT_TICK;
    public float MONSTER_MOVEMENT_TICK_HUNT;
    public float MONSTER_MOVEMENT_TICK_SEARCH;

    public float PLAYER_HEALTH;
    public float MONSTER_HEALTH;

    public float MONSTER_FORGETFULLNESS_TIMER;


    public float LIGHT_RADIUS;
    public float PLAYER_HEALTH_LOSS_RATE;
    public float MONSTER_HEALTH_LOSS_RATE;
    public float HEALTH_GAIN_RATE;
}
