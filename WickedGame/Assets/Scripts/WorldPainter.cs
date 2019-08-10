using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridHolder;

public static class WorldPainter
{
    public static GameObject parent;
    public static List<GameObject> walkableTiles;
    public static List<GameObject> unWalkableTiles;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadResources()
    {
        walkableTiles = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Tiles/Walkable"));
        unWalkableTiles = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Tiles/NotWalkable"));

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
                }
                else
                {
                    selectedObject = UnityEngine.Object.Instantiate(unWalkableTiles.GetRandom());
                }
                selectedObject.transform.SetParent(parent.transform);

                selectedObject.transform.position = new Vector3Int(x, 0, y);
            }
        }
    }

    public static void RemoveWorld()
    {
        MonoBehaviour.Destroy(parent);
    }
}
