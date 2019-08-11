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


    public float RESET_DELAY;
    //_______________________________________

    public float PLAYER_MOVEMENT_TICK;
    public float PLAYER_HEALTH;
    public float PLAYER_HEALTH_LOSS_RATE;
    public float PLAYER_HEALTH_GAIN_RATE;
    //_______________________________________

    public float LIGHT_RADIUS;
    public float LIGHT_FLICKER_SPEED;
    //_______________________________________
    public float MONSTER_GROWTH_FACTOR;
    public float MONSTER_WAIT_TIME;
    //_______________________________________
    public float MONSTER_MOVEMENT_TICK_HUNT;
    public float MONSTER_MOVEMENT_TICK_SEARCH;
    public float MONSTER_HEALTH;
    public float MONSTER_HEALTH_LOSS_RATE;

    public float MONSTER_DAMAGE;

    public Int2 BRANCH_AMOUNT;

    public float PLAYER_BURNING_EXTRA_SPEED_PERCENTAGE;

    public RuleSet CreateClone()
    {
        RuleSet newRuleSet = Instantiate(this);

        newRuleSet.GRID_WIDTH = GRID_WIDTH;
        newRuleSet.GRID_HEIGHT = GRID_HEIGHT;
        newRuleSet.OBSTACLE_AMOUNT = OBSTACLE_AMOUNT;
        newRuleSet.RESET_DELAY = RESET_DELAY;
        //_______________________________________
        newRuleSet.PLAYER_MOVEMENT_TICK = PLAYER_MOVEMENT_TICK;
        newRuleSet.PLAYER_HEALTH = PLAYER_HEALTH;
        newRuleSet.PLAYER_HEALTH_LOSS_RATE = PLAYER_HEALTH_LOSS_RATE;
        newRuleSet.PLAYER_HEALTH_GAIN_RATE = PLAYER_HEALTH_GAIN_RATE;
        //_______________________________________
        newRuleSet.LIGHT_RADIUS = LIGHT_RADIUS;
        newRuleSet.LIGHT_FLICKER_SPEED = LIGHT_FLICKER_SPEED;
        //_______________________________________
        newRuleSet.MONSTER_GROWTH_FACTOR = MONSTER_GROWTH_FACTOR;
        newRuleSet.MONSTER_WAIT_TIME = MONSTER_WAIT_TIME;
        //_______________________________________
        newRuleSet.MONSTER_MOVEMENT_TICK_HUNT = MONSTER_MOVEMENT_TICK_HUNT;
        newRuleSet.MONSTER_MOVEMENT_TICK_SEARCH = MONSTER_MOVEMENT_TICK_SEARCH;
        newRuleSet.MONSTER_HEALTH = MONSTER_HEALTH;
        newRuleSet.MONSTER_HEALTH_LOSS_RATE = MONSTER_HEALTH_LOSS_RATE;
        newRuleSet.MONSTER_DAMAGE = MONSTER_DAMAGE;


        return newRuleSet;
    }
}
