using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public Unit myUnit;
    float maxHealth;
    float tempMaxHealth;
    float currentHealth;

    public delegate void Dead();
    public Dead IsDead;

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void SetCurrentHealth(float currentHealth)
    {
        this.currentHealth = currentHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetTempMaxHealth()
    {
        return tempMaxHealth;
    }

    public void SetTempMaxHealth(float newMaxHealth)
    {
        tempMaxHealth = newMaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void GainHealth(float gain)
    {
        if (currentHealth < tempMaxHealth)
        {
            if ((currentHealth + gain) > tempMaxHealth)
            {
                currentHealth = tempMaxHealth;
            }
            else
            {
                currentHealth += gain;
            }
        }
    }

    public bool CheckIfHealthIsZero(float health)//if Unit dies, send Unit to GameManager.EvaluateGameState
    {
        if (health <= 0)
        {
            return true;
        }

        return false;
    }
}
