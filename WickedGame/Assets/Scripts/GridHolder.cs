using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Rules;

public static class GridHolder
{
    private static Cell[,] grid;

    public static Cell[,] GetWalkableGrid()
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
                grid[x, y].x = x;
                grid[x, y].y = y;
                grid[x, y].cellType = Cell.CellType.Basic;
                asList.Add(grid[x, y]);


                if (x == 0 || y == 0 || x == ruleSet.GRID_WIDTH - 1 || y == ruleSet.GRID_HEIGHT - 1)
                {
                    grid[x, y].walkable = false;
                }
            }
        }

        asList = asList.OrderBy(x => Guid.NewGuid()).ToList();
        List<Cell> obstacles = asList.Take(ruleSet.OBSTACLE_AMOUNT.GetRandomValue()).Where(x => x.walkable).ToList();

        foreach (var item in obstacles)
        {
            item.walkable = false;
        }


    }



}
