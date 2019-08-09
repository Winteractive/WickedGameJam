using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    float playerMaxHealth;//in Health class
    float playerTempMaxHealth;
    public float playerCurrentHealth;
    float monsterMaxHealth;
    public float monsterCurrentHealth;

    public bool inLight;//in player class

    public float burningDamage = 1;//in monster class
    public float darknessDamage = 1;//in player class
    public float healingPoints = 1;

    private void OnTriggerEnter2D(Collider2D monster)//in player class
    {
        inLight = true;
    }

    private void OnTriggerExit2D(Collider2D monster)//in player class
    {
        inLight = false;
    }
}
