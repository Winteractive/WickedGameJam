using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public int x;
    public int y;
    public float speed;
    public float health;


    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float value)
    {
        health = value;
    }

    public void RemoveHealth(float amount)
    {
        amount = Mathf.Abs(amount);
        health -= amount;
    }
}
