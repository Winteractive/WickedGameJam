using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;

public class GameManager : MonoBehaviour
{

    public static GameManager INSTANCE;

    public void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            if (INSTANCE != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Start()
    {
        GridHolder.GenerateGrid();
        WorldPainter.PaintWorld();
    }
}
