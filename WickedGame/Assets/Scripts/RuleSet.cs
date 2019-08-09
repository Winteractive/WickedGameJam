using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleSet", menuName = "RuleSet", order = 0)]
public class RuleSet : ScriptableObject
{

    public int GRID_WIDTH;
    public int GRID_HEIGHT;

    public Int2 OBSTACLE_AMOUNT;

    public float PLAYER_SPEED;
    public float PLAYER_HEALTH;
    public float MONSTER_SPEED;
    public float LIGHT_RADIUS;
    public float HEALTH_LOSS_RATE;
    public float HEALTH_GAIN_RATE;
}
