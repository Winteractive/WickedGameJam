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

    public void SetCurrentHealth(float newHealth)
    {
        this.currentHealth = newHealth;
        CheckIfHealthIsZero();
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
        CheckIfHealthIsZero();
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

    public void CheckIfHealthIsZero()
    {
        if (currentHealth <= 0)
        {
            IsDead?.Invoke();
        }
    }
}
