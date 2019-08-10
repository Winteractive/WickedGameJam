using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MatrixExtensions
{
    public static List<T> AsList<T>(this T[,] matrix)
    {
        List<T> list = new List<T>();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                list.Add(matrix[i, j]);
            }
        }
        return list;
    }
}
