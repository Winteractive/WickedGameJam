using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridHolder;
using static Rules;

public static class WorldPainter
{
    public static GameObject parent;
    public static List<GameObject> walkableTiles;
    public static List<GameObject> unWalkableTiles;
    public static List<GameObject> walls;
    public static List<GameObject> corners;
    public static List<GameObject> branches;
    public static List<GameObject> stoneDeco;
    public static List<GameObject> trees;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadResources()
    {
        walkableTiles = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Tiles/Walkable"));
        unWalkableTiles = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Tiles/NotWalkable"));
        walls = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Walls"));
        corners = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Corners"));
        branches = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Branches"));
        stoneDeco = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/StoneDecoration"));
        trees = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Trees"));
    }

    internal static void PaintWorld()
    {
        parent = new GameObject();
        Cell[,] cellsInWorld = GetGrid();
        for (int x = 0; x < cellsInWorld.GetLength(0); x++)
        {
            for (int y = 0; y < cellsInWorld.GetLength(1); y++)
            {
                GameObject selectedObject = null;
                if (cellsInWorld[x, y].walkable)
                {
                    selectedObject = UnityEngine.Object.Instantiate(walkableTiles.GetRandom());
                    if (cellsInWorld[x, y].cellType == Cell.CellType.Branch)
                    {
                        ServiceLocator.GetDebugProvider().Log("branch spawned");
                        GameObject branch = UnityEngine.Object.Instantiate(branches.GetRandom());
                        branch.transform.SetParent(parent.transform);
                        branch.transform.position = new Vector3(x, 0f, y);
                    }
                    else
                    {
                        if (UnityEngine.Random.value < 0.45f)
                        {
                            GameObject stoneDecoration = UnityEngine.Object.Instantiate(stoneDeco.GetRandom());
                            stoneDecoration.transform.SetParent(parent.transform);
                            int rotationTimes = UnityEngine.Random.Range(0, 4);
                            stoneDecoration.transform.Rotate(new Vector3(0, 90 * rotationTimes, 0));
                            stoneDecoration.transform.position = new Vector3(x, 0f, y);
                        }

                    }
                }
                else
                {
                    if (x == 0 || x == ruleSet.GRID_WIDTH - 1) // edge
                    {
                        if (y == 0) // corner
                        {
                            selectedObject = UnityEngine.Object.Instantiate(corners.GetRandom());
                            if (x == 0)
                            {
                                selectedObject.transform.Rotate(new Vector3(0, -180, 0));

                            }
                            else
                            {
                                selectedObject.transform.Rotate(new Vector3(0, 90, 0));

                            }
                            GameObject tileUnderUnwalkable = UnityEngine.Object.Instantiate(walkableTiles.GetRandom());
                            tileUnderUnwalkable.transform.SetParent(parent.transform);
                            tileUnderUnwalkable.transform.position = new Vector3Int(x, 0, y);
                        }
                        else if (y == ruleSet.GRID_HEIGHT - 1) // corner
                        {
                            selectedObject = UnityEngine.Object.Instantiate(corners.GetRandom());
                            if (x == 0)
                            {
                                selectedObject.transform.Rotate(new Vector3(0, -90, 0));
                            }
                            GameObject tileUnderUnwalkable = UnityEngine.Object.Instantiate(walkableTiles.GetRandom());
                            tileUnderUnwalkable.transform.SetParent(parent.transform);
                            tileUnderUnwalkable.transform.position = new Vector3Int(x, 0, y);
                        }
                        else // wall
                        {
                            selectedObject = UnityEngine.Object.Instantiate(walls.GetRandom());
                            selectedObject.transform.Rotate(new Vector3(0, 90, 0));
                           GameObject tileUnderUnwalkable = UnityEngine.Object.Instantiate(walkableTiles.GetRandom());
                           tileUnderUnwalkable.transform.SetParent(parent.transform);
                           tileUnderUnwalkable.transform.position = new Vector3Int(x, 0, y);

                        }
                    }
                    else if (y == 0 || y == ruleSet.GRID_HEIGHT - 1)
                    {
                        if (x == 0) // corner already placed
                        {


                        }
                        else if (x == ruleSet.GRID_WIDTH - 1) // corner already placed
                        {

                        }
                        else // wall
                        {
                            selectedObject = UnityEngine.Object.Instantiate(walls.GetRandom());
                            GameObject tileUnderUnwalkable = UnityEngine.Object.Instantiate(walkableTiles.GetRandom());
                            tileUnderUnwalkable.transform.SetParent(parent.transform);
                            tileUnderUnwalkable.transform.position = new Vector3Int(x, 0, y);
                        }
                    }
                    else
                    {
                        selectedObject = UnityEngine.Object.Instantiate(unWalkableTiles.GetRandom());
                        int rotationTimes = UnityEngine.Random.Range(0, 4);
                        selectedObject.transform.Rotate(new Vector3(0, 90 * rotationTimes, 0));
                        GameObject tileUnderUnwalkable = UnityEngine.Object.Instantiate(walkableTiles.GetRandom());
                        tileUnderUnwalkable.transform.SetParent(parent.transform);
                        tileUnderUnwalkable.transform.position = new Vector3Int(x, 0, y);
                    }

                }
                if (selectedObject != null)
                {
                    selectedObject.transform.SetParent(parent.transform);

                    selectedObject.transform.position = new Vector3Int(x, 0, y);
                }

            }
        }

        for (int x = -4; x < cellsInWorld.GetLength(0) + 3; x++)
        {
            for (int y = -4; y < cellsInWorld.GetLength(1) + 3; y++)
            {
                

                if (x == -1 || y == -1 || x == cellsInWorld.GetLength(0) || y == cellsInWorld.GetLength(1))
                {
                    if (UnityEngine.Random.value < 0.7f)
                    {
                        GameObject tree = UnityEngine.Object.Instantiate(unWalkableTiles.GetRandom());
                        tree.transform.SetParent(parent.transform);
                        tree.transform.position = new Vector3Int(x, 0, y);
                        int rotationTimes = UnityEngine.Random.Range(0, 4);
                        tree.transform.Rotate(new Vector3(0, 90 * rotationTimes, 0));
                        GameObject tileUnderUnwalkable = UnityEngine.Object.Instantiate(walkableTiles.GetRandom());
                        tileUnderUnwalkable.transform.SetParent(parent.transform);
                        tileUnderUnwalkable.transform.position = new Vector3Int(x, 0, y);
                    }

                }

                if (x == -2 || y == -2 || x == cellsInWorld.GetLength(0) + 1 || y == cellsInWorld.GetLength(1) + 1)
                {
                    if (UnityEngine.Random.value < 0.5f)
                    {
                        GameObject tree = UnityEngine.Object.Instantiate(unWalkableTiles.GetRandom());
                        tree.transform.SetParent(parent.transform);
                        tree.transform.position = new Vector3Int(x, 0, y);
                        int rotationTimes = UnityEngine.Random.Range(0, 4);
                        tree.transform.Rotate(new Vector3(0, 90 * rotationTimes, 0));
                        GameObject tileUnderUnwalkable = UnityEngine.Object.Instantiate(walkableTiles.GetRandom());
                        tileUnderUnwalkable.transform.SetParent(parent.transform);
                        tileUnderUnwalkable.transform.position = new Vector3Int(x, 0, y);

                    }
                }

                if (x == -3 || y == -3 || x == cellsInWorld.GetLength(0) + 2 || y == cellsInWorld.GetLength(1) + 2)
                {
                    if (UnityEngine.Random.value < 0.25f)
                    {
                        GameObject tree = UnityEngine.Object.Instantiate(unWalkableTiles.GetRandom());
                        tree.transform.SetParent(parent.transform);
                        tree.transform.position = new Vector3Int(x, 0, y);
                        int rotationTimes = UnityEngine.Random.Range(0, 4);
                        tree.transform.Rotate(new Vector3(0, 90 * rotationTimes, 0));
                        GameObject tileUnderUnwalkable = UnityEngine.Object.Instantiate(walkableTiles.GetRandom());
                        tileUnderUnwalkable.transform.SetParent(parent.transform);
                        tileUnderUnwalkable.transform.position = new Vector3Int(x, 0, y);

                    }
                }


                if (x == -4 || y == -4 || x == cellsInWorld.GetLength(0) + 3 || y == cellsInWorld.GetLength(1) + 3)
                {
                    if (UnityEngine.Random.value < 0.1f)
                    {
                        GameObject tree = UnityEngine.Object.Instantiate(unWalkableTiles.GetRandom());
                        tree.transform.SetParent(parent.transform);
                        tree.transform.position = new Vector3Int(x, 0, y);
                        int rotationTimes = UnityEngine.Random.Range(0, 4);
                        tree.transform.Rotate(new Vector3(0, 90 * rotationTimes, 0));
                        GameObject tileUnderUnwalkable = UnityEngine.Object.Instantiate(walkableTiles.GetRandom());
                        tileUnderUnwalkable.transform.SetParent(parent.transform);
                        tileUnderUnwalkable.transform.position = new Vector3Int(x, 0, y);

                    }
                }
            }
        }
    }

    public static void RemoveWorld()
    {
        MonoBehaviour.Destroy(parent);
    }
}
