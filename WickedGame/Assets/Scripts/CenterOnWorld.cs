using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;

public class CenterOnWorld : MonoBehaviour
{
    void Start()
    {
        this.transform.position = new Vector3((ruleSet.GRID_WIDTH / 2), 9, 1);
    }


}
