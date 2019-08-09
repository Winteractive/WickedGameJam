using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;
public class Ground : MonoBehaviour
{
    void Start()
    {
        this.transform.localScale = new Vector3(ruleSet.GRID_WIDTH + 1, ruleSet.GRID_HEIGHT + 1, 0.1f);
        this.transform.position = new Vector3(ruleSet.GRID_WIDTH / 2, -0.1f, ruleSet.GRID_HEIGHT / 2);
    }


}
