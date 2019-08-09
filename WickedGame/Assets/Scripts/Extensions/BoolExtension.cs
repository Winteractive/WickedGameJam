using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoolExtension
{
    public static int AsInt(this bool b)
    {
        return b ? 1 : -1;
    }
}