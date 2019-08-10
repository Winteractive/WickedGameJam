using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableVisualisation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {

        if (Application.isPlaying == false)
        {
            return;
        }
        if (GridHolder.GetGrid() == null)
        {
            return;
        }
        foreach (Cell cell in GridHolder.GetGrid())
        {
            if (cell.walkable)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(cell.AsVector3Int(), 0.4f);
        }

    }
}
