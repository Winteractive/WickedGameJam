using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public bool walkable;
    public enum CellType { Basic, Trap_Up, Trap_Down, Trap_Left, Trap_Right };
    public CellType cellType;
    public int x;
    public int y;


    public int h;
    public int f;
    public int g;
    public List<Cell> neighbours;
    public Cell pfParent;

    public Vector3Int AsVector3Int()
    {
        return Vector3Int.right * x + Vector3Int.up * y;
    }

    public void SetNeighbours(List<Cell> list)
    {
        neighbours = list;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
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
