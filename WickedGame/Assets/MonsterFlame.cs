using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlame : MonoBehaviour
{
    public Light _light;
    ParticleSystem sys;
    ParticleSystem.MainModule main;
    ParticleSystem.EmissionModule emi;
    void Start()
    {
        sys = GetComponent<ParticleSystem>();
        main = sys.main;
        emi = sys.emission;
    }


    void Update()
    {
        main.startColor = _light.color;
    }


    public void ChangeMode(Monster.Modes mode)
    {
        switch (mode)
        {
            case Monster.Modes.Search:
                main.startSize = new ParticleSystem.MinMaxCurve(6, 8);
                emi.rateOverTime = 40;
                main.startRotation = new ParticleSystem.MinMaxCurve(-45f, 45f);
                break;
            case Monster.Modes.Hunt:
                main.startSize = new ParticleSystem.MinMaxCurve(15, 20);
                emi.rateOverTime = 80;
                main.startRotation = new ParticleSystem.MinMaxCurve(-7f, 7f);
                break;
            case Monster.Modes.Happy:
                main.startSize = new ParticleSystem.MinMaxCurve(6, 8);
                main.startRotation = new ParticleSystem.MinMaxCurve(-45f, 45f);
                emi.rateOverTime = 40;
                break;
            case Monster.Modes.Dead:
                main.startSize = new ParticleSystem.MinMaxCurve(6, 8);
                main.startRotation = new ParticleSystem.MinMaxCurve(-45f, 45f);
                emi.rateOverTime = 0;
                break;
            default:
                break;
        }
    }
}
