using System;
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

    public delegate void HpDelegates(float value);
    public HpDelegates HpChanged;

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        tempMaxHealth = maxHealth;
    }

    public void SetCurrentHealth(float newHealth)
    {
        this.currentHealth = newHealth;
        CheckIfHealthIsZero();
        HpChanged?.Invoke(this.currentHealth);
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

    public void ReduceMaximumHealth(float amount)
    {
        tempMaxHealth -= amount;
        currentHealth = currentHealth > tempMaxHealth ? tempMaxHealth : currentHealth;
        HpChanged?.Invoke(this.currentHealth);

    }

    public void TakeDamage(float damage, bool reduceMaximum = false)
    {
        currentHealth -= damage;
        CheckIfHealthIsZero();

        HpChanged?.Invoke(this.currentHealth);

    }

    public void GainHealth(float gain)
    {
        gain = Mathf.Abs(gain);
        currentHealth += gain;

        currentHealth = currentHealth > tempMaxHealth ? tempMaxHealth : currentHealth;
        HpChanged?.Invoke(this.currentHealth);

    }

    public void CheckIfHealthIsZero()
    {
        if (currentHealth <= 0)
        {
            IsDead?.Invoke();
        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

}
