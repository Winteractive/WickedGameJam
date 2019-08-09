using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Pathfinding
{
    public static InputManager.Direction GetNextStepDirection(Unit mover, Unit target)
    {
        List<Cell> path = PathfindBetween(mover.pos.GetAsVector3Int(), target.pos.GetAsVector3Int());
        if (path == null)
        {
            ServiceLocator.GetDebugProvider().Log("no path found");
            return InputManager.Direction.NONE;
        }
        Cell closestCell = path.Last();
        return DirectionManager.GetDirectionBetween(mover.pos.GetAsVector3Int(), closestCell.AsVector3Int());
    }

    [RuntimeInitializeOnLoadMethod]
    public static void SetUp()
    {

        open = new List<Cell>();
        closed = new List<Cell>();
        allCells = new List<Cell>();





    }


    public enum Style { Manhattan, Diagonal, Euclidian };
    [HideInInspector]
    public static Style selectedStyle;

    static Cell startCell;
    static Cell destionationCell;
    static List<Cell> open;
    static List<Cell> closed;
    static List<Cell> allCells;
    static Cell currentCell;

    public static void SetAllCells(List<Cell> _allCells)
    {
        allCells = _allCells;
    }

    public static List<Vector2Int> ORTHOGONAL = new List<Vector2Int>()
    {
        {new Vector2Int(0,1)},
        {new Vector2Int(1,0)},
        {new Vector2Int(0,-1)},
        {new Vector2Int(-1,0)},
    };

    public static List<Vector2Int> DIAGONALS = new List<Vector2Int>()
    {
        {new Vector2Int(1,1)},
        {new Vector2Int(-1,-1)},
        {new Vector2Int(1,-1)},
        {new Vector2Int(-1,1)}
    };


    const int ONE_G_STEP = 10;
    const int ONE_DIAGONAL_G_STEP = 14;

    public enum CellPathfindingStates { start, open, closed, neighbour, current, path, goal };

    public const int NOPATH = 0;




    private static void CalculateNeighbours()
    {
        Debug.Log("calculate Neighbours");
        foreach (Cell pathfindingTile in allCells)
        {
            pathfindingTile.SetNeighbours(GetNeighbours(pathfindingTile));
        }
    }

    public static List<Cell> GetNeighbours(Cell cell)
    {


        List<Cell> neighbours = new List<Cell>();
        foreach (var direction in ORTHOGONAL)
        {
            Vector3Int center = cell.AsVector3Int();
            Vector3Int directionVector = Vector3Int.right * direction.x + Vector3Int.up * direction.y;
            Cell neighbour = GridHolder.GetCell(center + directionVector) as Cell;
            if (neighbour != null)
            {
                neighbours.Add(neighbour);

            }
        }

        if (selectedStyle == Style.Diagonal || selectedStyle == Style.Euclidian)
        {
            foreach (var direction in DIAGONALS)
            {
                Vector3Int center = cell.AsVector3Int();
                Vector3Int directionVector = Vector3Int.right * direction.x + Vector3Int.up * direction.y;
                Cell neighbour = GridHolder.GetCell(center + directionVector) as Cell;
                if (neighbour != null)
                {
                    neighbours.Add(neighbour);
                }
            }
        }

        return neighbours.Count > 0 ? neighbours : null;
    }


    private static void SetH(Cell theGoal)
    {
        foreach (Cell item in allCells)
        {
            CalculateH(item, theGoal);
        }
    }

    private static int CalculateGCost(Cell calculatedCell, Cell parentCell)
    {
        switch (selectedStyle)
        {
            case Style.Manhattan:
                return ONE_G_STEP;

            case Style.Diagonal:
                return DiagonalGCalculation(calculatedCell, parentCell);

            case Style.Euclidian:
                return DiagonalGCalculation(calculatedCell, parentCell);

            default:
                Debug.LogError("something went wrong");
                return ONE_G_STEP;

        }
    }

    public static void SwitchToManhattan()
    {
        selectedStyle = Style.Manhattan;
        CalculateNeighbours();
    }
    public static void SwitchToDiagonal()
    {
        selectedStyle = Style.Diagonal;
        CalculateNeighbours();
    }
    public static void SwitchToEuclidean()
    {
        selectedStyle = Style.Euclidian;
        CalculateNeighbours();
    }

    private static int DiagonalGCalculation(Cell calculatedCell, Cell parentCell)
    {
        int dis = 0;
        int xDis = Mathf.Abs(calculatedCell.GetX() - parentCell.GetX());
        int yDis = Mathf.Abs(calculatedCell.GetY() - parentCell.GetY());
        dis = xDis + yDis;

        if (dis == 2)
        {
            return ONE_DIAGONAL_G_STEP;
        }
        else if (dis == 1)
        {
            return ONE_G_STEP;
        }

        Debug.LogError("something went wrong");
        return 0;
    }

    private static void CalculateH(Cell cell, Cell goal)
    {
        int calculatedH = 0;

        int xDis = Mathf.Abs(goal.GetX() - cell.GetX());
        int yDis = Mathf.Abs(goal.GetY() - cell.GetY());


        switch (selectedStyle)
        {
            case Style.Manhattan:
                calculatedH = (xDis + yDis) * ONE_G_STEP;
                break;
            case Style.Diagonal:
                calculatedH = Mathf.Max(xDis, yDis) * ONE_DIAGONAL_G_STEP;
                break;
            case Style.Euclidian:
                calculatedH = (int)Mathf.Sqrt(Mathf.Pow(xDis, 2) + Mathf.Pow(yDis, 2)) * ONE_DIAGONAL_G_STEP;
                break;
            default:
                break;
        }


        cell.h = calculatedH;
    }

    private static Cell FindLowestFCostCell()
    {
        if (open.Count > 0)
        {
            Cell smallestFCell = open[0];
            int smallestF = smallestFCell.CalculateAndGetF();
            foreach (Cell cell in open)
            {
                if (cell.CalculateAndGetF() < smallestF)
                {
                    smallestF = cell.f;
                    smallestFCell = cell;
                }
            }

            return smallestFCell;
        }
        else
        {
            ServiceLocator.GetDebugProvider().Log("open list was empty");
            return null;
        }
    }

    public static List<Cell> PathfindBetween(Vector3Int start, Vector3Int goal)
    {
        return PathfindBetween(GridHolder.GetCell(start), GridHolder.GetCell(goal));
    }

    // public static void VisualizePath(Vector3Int start, Vector3Int goal)
    // {
    //     ServiceLocator.GetDebugProvider().Log("visualize path call");
    //     INSTANCE.StartCoroutine(INSTANCE.VisualizePathfinding(map.GetTile(start) as Cell, map.GetTile(goal) as Cell));
    // }

    // public IEnumerator VisualizePathfinding(Cell start, Cell goal)
    // {
    //     if (start == goal)
    //     {
    //         Debug.LogError("START WAS GOAL");
    //         yield break;
    //     }
    //     visualizationMap.ClearAllTiles();
    //     ServiceLocator.GetDebugProvider().Log("visualize pathfind between");
    //     ServiceLocator.GetDebugProvider().Log(start.AsVector3Int());
    //     ServiceLocator.GetDebugProvider().Log("and");
    //     ServiceLocator.GetDebugProvider().Log(goal.AsVector3Int());
    //
    //     int rescueBreak = 0;
    //     List<Cell> path = new List<Cell>();
    //     CalculateNeighbours();
    //     open.Clear();
    //     closed.Clear();
    //
    //     foreach (Cell cell in allCells)
    //     {
    //         cell.ResetPathfindingValues();
    //     }
    //
    //     SetH(goal);
    //
    //     open.Add(start);
    //
    //     do
    //     {
    //         rescueBreak++;
    //         ServiceLocator.GetDebugProvider().Log("loop");
    //         if (rescueBreak > 50)
    //         {
    //             ServiceLocator.GetDebugProvider().Log("no path found!!!");
    //             // return null;
    //         }
    //         Cell current = FindLowestFCostCell();
    //
    //
    //
    //         open.Remove(current);
    //         closed.Add(current);
    //         visualizationMap.SetTile(current.AsVector3Int(), searchedCell);
    //
    //         if (current.AsVector3Int() == goal.AsVector3Int())
    //         {
    //             Debug.LogError("Current and Goal on same square");
    //         }
    //
    //         if (current == goal)
    //         {
    //             path.Add(current);
    //             do
    //             {
    //                 ServiceLocator.GetDebugProvider().Log("add to path: " + current.AsVector3Int());
    //                 current = current.data.pfParent;
    //
    //
    //                 if (current == null)
    //                 {
    //                     Debug.LogError("current was null!");
    //                 }
    //
    //                 visualizationMap.SetTile(current.AsVector3Int(), pathCell);
    //
    //                 path.Add(current);
    //                 yield return new WaitForSeconds(1f);
    //             } while (current.data.pfParent != start);
    //
    //
    //             ServiceLocator.GetDebugProvider().Log("path found");
    //
    //             //  return path;
    //         }
    //
    //
    //         foreach (Cell neighbour in current.GetNeighbours())
    //         {
    //             if (closed.Contains(neighbour))
    //             {
    //                 continue;
    //             }
    //
    //             if (neighbour.IsViablePathfindingCell() == false)
    //             {
    //                 continue;
    //             }
    //
    //             if (open.Contains(neighbour) == false)
    //             {
    //                 open.Add(neighbour);
    //                 neighbour.data.pfParent = current;
    //                 neighbour.data.g = current.GetG() + CalculateGCost(neighbour, current);
    //
    //             }
    //             else
    //             {
    //                 if (current.GetG() + ONE_G_STEP < neighbour.GetG())
    //                 {
    //                     neighbour.data.pfParent = current;
    //                     neighbour.data.g = current.GetG() + CalculateGCost(neighbour, current);
    //                 }
    //             }
    //
    //         }
    //
    //         yield return new WaitForSeconds(0.05f);
    //     } while (open.Count > 0);
    //
    //     ServiceLocator.GetDebugProvider().Log("no more open cells");
    //     //return null;
    //
    // }

    public static List<Cell> PathfindBetween(Cell start, Cell goal)
    {

        if (start == goal)
        {
            ServiceLocator.GetDebugProvider().Log("pathfinding to own cell... Abort pathfinding");
            return null;
        }

        int rescueBreak = 0;
        List<Cell> path = new List<Cell>();
        CalculateNeighbours();
        open.Clear();
        closed.Clear();

        foreach (Cell cell in allCells)
        {
            cell.ResetPathfindingValues();
        }

        SetH(goal);

        open.Add(start);

        do
        {
            rescueBreak++;
            if (rescueBreak > 50)
            {
                ServiceLocator.GetDebugProvider().Log("no path found!!!");
                return null;
            }
            Cell current = FindLowestFCostCell();

            ServiceLocator.GetDebugProvider().Log(current.AsVector3Int());

            open.Remove(current);
            closed.Add(current);


            if (current == goal)
            {
                path.Add(current);
                do
                {
                    current = current.pfParent;
                    if (current == null)
                    {
                        Debug.LogError("current was null!");
                    }
                    path.Add(current);



                } while (current.pfParent != start);


                ServiceLocator.GetDebugProvider().Log("path found");

                return path;
            }


            foreach (Cell neighbour in current.GetNeighbours())
            {
                if (closed.Contains(neighbour))
                {
                    continue;
                }

                if (neighbour.IsViablePathfindingCell() == false)
                {
                    continue;
                }

                if (open.Contains(neighbour) == false)
                {
                    if (neighbour.IsViablePathfindingCell() || neighbour == goal)
                    {
                        open.Add(neighbour);
                        neighbour.pfParent = current;
                        neighbour.g = current.GetG() + CalculateGCost(neighbour, current);
                    }



                }
                else
                {
                    if (current.GetG() + ONE_G_STEP < neighbour.GetG())
                    {
                        neighbour.pfParent = current;
                        neighbour.g = current.GetG() + CalculateGCost(neighbour, current);
                    }
                }

            }

        } while (open.Count > 0);

        return null;

    }

}
