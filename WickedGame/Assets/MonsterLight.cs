using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;
[RequireComponent(typeof(Light))]
public class MonsterLight : MonoBehaviour
{
    Light light;
    float intensity;
    float time;

    public Gradient lifeGradient;
    private void Start()
    {
        light = GetComponent<Light>();
        light.range = ruleSet.LIGHT_RADIUS;
        intensity = light.intensity;
        light.color = lifeGradient.Evaluate(0);
    }

    private void Update()
    {
        time += Time.deltaTime * ruleSet.LIGHT_FLICKER_SPEED;

        light.intensity = intensity + Mathf.Sin(time);
    }

    public void UpdateColor(float currentHealth, float maxHealth)
    {
        float val = (currentHealth / maxHealth);
        light.color = lifeGradient.Evaluate(1 - val);
    }
}
