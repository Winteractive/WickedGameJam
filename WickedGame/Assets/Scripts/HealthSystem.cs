using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    HealthSystem INSTANCE;

    float maxHealth;
    float tempMaxHealth;
    public float currentHealth;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        tempMaxHealth = maxHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
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
}
