using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;
public class InLightChecker : MonoBehaviour
{

    public bool inLight;
    public LayerMask monsterMask;
    List<Monster> monsters;


    RaycastHit hit;

    private void Start()
    {
        monsters = new List<Monster>();
        monsters.AddRange(FindObjectsOfType<Monster>());
    }


    void FixedUpdate()
    {
        CheckInLight();
    }

    private void CheckInLight()
    {
        inLight = false;
        foreach (Monster monster in monsters)
        {
            if (Vector3.Distance(transform.position, monster.gameObject.transform.position) <= ruleSet.LIGHT_RADIUS)
            {
                ServiceLocator.GetDebugProvider().Log("in range");
                if (Physics.Linecast(transform.position, monster.transform.position, out hit, monsterMask))
                {
                    ServiceLocator.GetDebugProvider().Log("linecast hit");
                    if (hit.collider.gameObject == monster.gameObject)
                    {
                        ServiceLocator.GetDebugProvider().Log("in light");
                        inLight = true;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Monster monster in monsters)
        {
            Gizmos.DrawWireSphere(monster.pos.GetAsBoardAlignedVector3Int(), ruleSet.LIGHT_RADIUS);
        }
    }
}
