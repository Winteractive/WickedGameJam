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
    public Vector3Int GetAsBoardAlignedVector3Int()
    {
        return Vector3Int.right * x + new Vector3Int(0, 0, 1) * y;
    }
    public bool IsSame(Int2 otherPos)
    {
        return (x == otherPos.x && y == otherPos.y);
    }
}
