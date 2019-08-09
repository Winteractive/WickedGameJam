using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    HealthSystem INSTANCE;

    public delegate void DeathDelegate(Entity entity);
    DeathDelegate deathDelegate;

    float nextTickTime = 1f;
    public float tick = 0.1f;

    public enum Entity { Player, Monster };

    float playerMaxHealth;
    float playerTempMaxHealth;
    public float playerCurrentHealth;
    float monsterMaxHealth;
    public float monsterCurrentHealth;

    public bool inLight;

    public float burningDamage = 1;
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

    private void Start()
    {
        playerTempMaxHealth = playerMaxHealth;
    }

    private void Update()
    {
        if (inLight)
        {
            if (Time.time > nextTickTime)
            {
                nextTickTime += tick;
                TakeDamage(darknessDamage, Entity.Player);
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

        if (Time.time > nextTickTime)
        {
            nextTickTime += tick;
            TakeDamage(darknessDamage, Entity.Monster);
        }

        if (playerCurrentHealth <= 0)
        {
            deathDelegate?.Invoke(Entity.Player);
        }
        else if (monsterCurrentHealth <= 0)
        {
            deathDelegate?.Invoke(Entity.Monster);
        }
    }

    private void OnTriggerEnter2D(Collider2D monster)
    {
        inLight = true;
    }

    private void OnTriggerExit2D(Collider2D monster)
    {
        inLight = false;
    }

    public float GetTempMaxHealth()
    {
        return playerTempMaxHealth;
    }

    public void SetTempMaxHealth(float newMaxHealth)
    {
        playerTempMaxHealth = newMaxHealth;
    }

    public void TakeDamage(float damage, Entity entity)
    {
        switch (entity)
        {
            case Entity.Player:
                playerCurrentHealth -= damage;
                break;
            case Entity.Monster:
                monsterCurrentHealth -= damage;
                break;
            default:
                break;
        }
    }

    public void GainHealth(float gain)
    {
        if (playerCurrentHealth < playerTempMaxHealth)
        {
            if ((playerCurrentHealth + gain) > playerTempMaxHealth)
            {
                playerCurrentHealth = playerTempMaxHealth;
            }
            else
            {
                playerCurrentHealth += gain;
            }
        }
    }

    public void Death(Entity entity)
    {
        switch (entity)
        {
            case Entity.Player:
                GameManager.INSTANCE.gameOverDelegate(Entity.Player);
                break;
            case Entity.Monster:
                GameManager.INSTANCE.gameOverDelegate(Entity.Monster);
                break;
            default:
                break;
        }
    }
}
