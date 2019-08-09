using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Int2
{
    public int x;
    public int y;
    public int GetRandomValue()
    {
        return Random.Range(x, y);
    }
    public Vector3Int GetAsVector3Int()
    {
        return Vector3Int.right * x + Vector3Int.up * y;
    }
}
