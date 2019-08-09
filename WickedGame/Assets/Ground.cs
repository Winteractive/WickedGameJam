using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;
public class Ground : MonoBehaviour
{
    void Start()
    {
        this.transform.position = new Vector3(ruleSet.GRID_WIDTH, -0.1f, ruleSet.GRID_HEIGHT);
    }


}
