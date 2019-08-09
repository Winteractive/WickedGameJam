using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    HealthSystem INSTANCE;

    float nextTickTime = 1f;
    public float tick = 0.1f;

    float maxHealth;
    float tempMaxHealth;
    public float currentHealth;

    public bool inLight;

    public float darknessDamage = 1;
    public float healingPoints = 1;

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

    private void Update()
    {
        if (inLight)
        {
            if (Time.time > nextTickTime)
            {
                nextTickTime += tick;
                TakeDamage(darknessDamage);
            }
        }
        else if (!inLight)
        {
            if (Time.time > nextTickTime)
            {
                nextTickTime += tick;
                GainHealth(healingPoints);
            }
        }
    }

    private void Start()
    {
        tempMaxHealth = maxHealth;
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
