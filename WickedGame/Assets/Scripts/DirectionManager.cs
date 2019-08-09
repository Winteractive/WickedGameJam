using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputManager;

public static class DirectionManager
{
    public static Direction GetDirectionBetween(Vector3Int Start, Vector3Int end)
    {
        if (Start.x != end.x && Start.y != end.y)
        {
            Debug.LogError("positions not on same row or collumn");
            return Direction.NONE;
        }

        if (Start.x == end.x && Start.y == end.y)
        {
            Debug.LogError("positions were the same");
            return Direction.NONE;
        }

        if (Start.x == end.x)
        {
            if (Start.y > end.y)
            {
                return Direction.Down;
            }
            else
            {
                return Direction.Up;
            }
        }
        else
        {
            if (Start.x > end.x)
            {
                return Direction.Left;
            }
            else
            {
                return Direction.Right;
            }
        }
    }
}
