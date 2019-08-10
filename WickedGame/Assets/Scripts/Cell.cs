using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public bool walkable;
    public enum CellType { Basic, Trap_Up, Trap_Down, Trap_Left, Trap_Right, Branch };
    public CellType cellType;
    public Int2 pos;


    public int h;
    public int f;
    public int g;
    public List<Cell> neighbours;
    public Cell pfParent;

    public Vector3Int AsVector3Int()
    {
        return Vector3Int.right * pos.x + Vector3Int.up * pos.y;
    }

    public void SetNeighbours(List<Cell> list)
    {
        neighbours = list;
    }

    public int GetX()
    {
        return pos.x;
    }

    public int GetY()
    {
        return pos.y;
    }



    public int CalculateAndGetF()
    {
        f = g + h;
        return f;
    }

    public void ResetPathfindingValues()
    {
        h = 0;
        g = 0;
        pfParent = null;
    }

    public List<Cell> GetNeighbours()
    {
        return neighbours;
    }

    public bool IsViablePathfindingCell()
    {
        return walkable;
    }

    public int GetG()
    {
        return g;
    }
}
