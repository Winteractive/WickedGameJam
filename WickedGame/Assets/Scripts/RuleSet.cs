using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleSet", menuName = "RuleSet", order = 0)]
public class RuleSet : ScriptableObject
{

    public int GRID_WIDTH;
    public int GRID_HEIGHT;

    public Int2 OBSTACLE_AMOUNT;
    public Int2 MONSTER_SEARCH_TIMER;

    public float PLAYER_MOVEMENT_TICK;
    public float PLAYER_HEALTH;
    public float PLAYER_HEALTH_LOSS_RATE;
    public float PLAYER_HEALTH_GAIN_RATE;

    public float MONSTER_GROWTH_FACTOR;
    public float LIGHT_RADIUS;
    public float MONSTER_FORGETFULLNESS_TIMER;
    public float MONSTER_WAIT_TIME;

    public float MONSTER_MOVEMENT_TICK_HUNT;
    public float MONSTER_MOVEMENT_TICK_SEARCH;
    public float MONSTER_HEALTH;
    public float MONSTER_HEALTH_LOSS_RATE;
    public float LIGHT_FLICKER_SPEED;

    public RuleSet CreateClone()
    {
        RuleSet newRuleSet = Instantiate(this);

        newRuleSet.GRID_WIDTH = GRID_WIDTH;
        newRuleSet.GRID_HEIGHT = GRID_HEIGHT;
        newRuleSet.OBSTACLE_AMOUNT = OBSTACLE_AMOUNT;
        newRuleSet.PLAYER_MOVEMENT_TICK = PLAYER_MOVEMENT_TICK;
        newRuleSet.MONSTER_MOVEMENT_TICK_HUNT = MONSTER_MOVEMENT_TICK_HUNT;
        newRuleSet.MONSTER_MOVEMENT_TICK_SEARCH = MONSTER_MOVEMENT_TICK_SEARCH;
        newRuleSet.PLAYER_HEALTH = PLAYER_HEALTH;
        newRuleSet.MONSTER_HEALTH = MONSTER_HEALTH;
        newRuleSet.MONSTER_FORGETFULLNESS_TIMER = MONSTER_FORGETFULLNESS_TIMER;
        newRuleSet.LIGHT_RADIUS = LIGHT_RADIUS;
        newRuleSet.PLAYER_HEALTH_LOSS_RATE = PLAYER_HEALTH_LOSS_RATE;
        newRuleSet.MONSTER_HEALTH_LOSS_RATE = MONSTER_HEALTH_LOSS_RATE;
        newRuleSet.PLAYER_HEALTH_GAIN_RATE = PLAYER_HEALTH_GAIN_RATE;
        newRuleSet.MONSTER_WAIT_TIME = MONSTER_WAIT_TIME;

        return newRuleSet;
    }
}
