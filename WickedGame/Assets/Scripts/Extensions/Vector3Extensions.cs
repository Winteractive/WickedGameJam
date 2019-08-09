using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions
{

    public static Vector3 X(this Vector3 vec, float _x)
    {
        vec = new Vector3(_x, vec.y, vec.z);
        return vec;
    }
    public static Vector3 Y(this Vector3 vec, float _y)
    {
        vec = new Vector3(vec.x, _y, vec.z);
        return vec;
    }
    public static Vector3 Z(this Vector3 vec, float _z)
    {
        vec = new Vector3(vec.x, vec.y, _z);
        return vec;
    }

 

    public static Vector3Int GetAsVector3Int(this Vector3 vec)
    {
        return new Vector3Int(Mathf.FloorToInt(vec.x), Mathf.FloorToInt(vec.y), Mathf.FloorToInt(vec.z));
    }

    public static RectTransform Z(this RectTransform rec, float _z)
    {
        rec.position = new Vector3(rec.position.x, rec.position.y, _z);
        return rec;
    }

    public static RectTransform Y(this RectTransform rec, float _y)
    {
        rec.position = new Vector3(rec.position.x, _y, rec.position.z);
        return rec;
    }

    public static RectTransform X(this RectTransform rec, float _x)
    {
        rec.position = new Vector3(_x, rec.position.y, rec.position.z);
        return rec;
    }

    public static float GetDistanceWithoutZ(this float distance, Vector3 A, Vector3 B)
    {
        Vector3 _A = A;
        Vector3 _B = B;

        _A = _A.Z(0);
        _B = _B.Z(0);

        distance = Vector3.Distance(_A, _B);

        return distance;

    }



}
