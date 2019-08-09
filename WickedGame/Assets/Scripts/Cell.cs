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
}
