using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{

    public static void UniformScale(this Transform tra, float amount)
    {
        tra.localScale = tra.localScale * amount;


    }
    public static void UniformScaleFrom(this Transform tra, Vector3 from, float amount)
    {
        tra.localScale = from * amount;
    }

    public static void DestroyItween(this GameObject obj)
    {
        if (obj.GetComponent<iTween>())
        {
            Object.Destroy(obj.GetComponent<iTween>());
        }
    }
}
