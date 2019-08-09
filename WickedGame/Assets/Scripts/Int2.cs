using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Int2
{
    public int A;
    public int B;
    public int GetRandomValue()
    {
        return Random.Range(A, B);
    }
}
