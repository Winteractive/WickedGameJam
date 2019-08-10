using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Rules;

public static class GridHolder
{
    private static Cell[,] grid;

    public static Cell[,] GetGrid()
    {
        return grid;
    }

    public static void SetWalkableCell(int x, int y, bool walkable)
    {
        if (x < 0 || y < 0 || x > ruleSet.GRID_WIDTH || y > ruleSet.GRID_HEIGHT)
        {
            Debug.LogError("Cell: " + "x: " + x + "y: " + y + "is outside grid range");
        }
        grid[x, y].walkable = walkable;
    }

    public static bool IsWalkable(int x, int y)
    {
        if (!IsInsideWorld(x, y))
        {
            return false;
        }
        return grid[x, y].walkable;
    }

    public static bool IsWalkable(Vector3Int pos)
    {
        return IsWalkable(pos.x, pos.y);
    }

    public static Cell GetCell(int x, int y)
    {
        if (!IsInsideWorld(x, y))
        {
            return null;
        }

        return grid[x, y];
    }

    public static Cell GetCell(Vector3Int pos)
    {
        return GetCell(pos.x, pos.y);
    }

    public static bool IsInsideWorld(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            return false;
        }
        if (x > ruleSet.GRID_WIDTH - 1)
        {
            return false;
        }
        if (y > ruleSet.GRID_HEIGHT - 1)
        {
            return false;
        }

        return true;
    }

    public static bool IsInsideWorld(Vector3Int pos)
    {
        return IsInsideWorld(pos.x, pos.y);
    }

    public static void GenerateGrid()
    {

        List<Cell> asList = new List<Cell>();
        grid = new Cell[ruleSet.GRID_WIDTH, ruleSet.GRID_HEIGHT];
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x, y] = new Cell();
                grid[x, y].walkable = true;
                grid[x, y].pos.x = x;
                grid[x, y].pos.y = y;
                grid[x, y].cellType = Cell.CellType.Basic;
                asList.Add(grid[x, y]);


                if (x == 0 || y == 0 || x == ruleSet.GRID_WIDTH - 1 || y == ruleSet.GRID_HEIGHT - 1)
                {
                    grid[x, y].walkable = false;
                }
            }
        }

        asList = asList.OrderBy(x => Guid.NewGuid()).ToList();
        List<Cell> obstacles = asList.Where(x => x.walkable).Take(ruleSet.OBSTACLE_AMOUNT.GetRandomValue()).ToList();

        foreach (var item in obstacles)
        {
            item.walkable = false;
        }

        Pathfinding.SetAllCells(asList);


    }

    internal static Cell GetRandomWalkableCell()
    {
        return grid.AsList().Where(x => x.walkable).OrderBy(x => Guid.NewGuid()).First();
    }
}
