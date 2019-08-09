using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3IntExtensions
{
    public static Vector3 GetAsVector3(this Vector3Int vector3Int)
    {
        return Vector3.right * vector3Int.x + Vector3.up * vector3Int.y + Vector3.forward * vector3Int.z;
    }
}
