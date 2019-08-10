using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlame : MonoBehaviour
{
    public Light _light;
    ParticleSystem sys;
    ParticleSystem.MainModule main;
    void Start()
    {
        sys = GetComponent<ParticleSystem>();
        main = sys.main;
    }


    void Update()
    {
        main.startColor = _light.color;
    }
}
