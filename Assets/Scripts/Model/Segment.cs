//using System;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public float length;

    public int Rows;
    public int Columns;

    public int[,] Fill;

    public float width;

    public Vector3 GetPos(int i, int j, float AddY = 0)
    {
        return new Vector3((i + 0.5f) * width / Columns - width / 2, AddY, j * length / Rows);
    }

    public void SetFill(int[,] fill, float w)
    {
        Rows = fill.GetLength(1);
        Columns = fill.GetLength(0);
        Fill = new int[Columns, Rows];
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                Fill[i, j] = fill[i, j];
            }
        }
        width = w;
    }
}
