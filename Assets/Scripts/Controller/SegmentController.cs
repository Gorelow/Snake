using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentController : MonoBehaviour
{
    public Segment model;

    public void Set(int[,] fill, EntityView correct, EntityView incorrect)
    {
        model.SetFill(fill,Track.Instance.Width);
        for (int i = 0; i < model.Columns; i++)
        {
            for (int j = 0; j < model.Rows; j++)
            {
                if ((model.Fill[i, j] == 1) || (model.Fill[i, j] == -1))
                {
                    var c = Instantiate(model.Fill[i, j] == 1 ? correct : incorrect, transform);
                    c.transform.localPosition = model.GetPos(i, j, 0.9f);
                }
            }
        }
    }

    public void Set(int[,] fill, GroupOfPeople gop, ColoringLine coloringLine)
    {
        int colors = Colorer.Colors.Count;
        model.SetFill(fill, Track.Instance.Width);
        int correct = Random.Range(0, colors);
        int incorrect = (correct + Random.Range(1, colors)) % colors;
        for (int i = 0; i < model.Columns; i++)
        {
            for (int j = 0; j < model.Rows; j++)
            {
                if ((model.Fill[i, j] == 1) || (model.Fill[i, j] == -1))
                {
                    var c = Instantiate(gop, transform);
                    c.transform.localPosition = model.GetPos(i, j);
                    c.Activate(model.Fill[i, j] == 1 ? correct : incorrect);
                }
            }
        }
        var line = Instantiate(coloringLine, transform);
        line.Load(correct);
    }
}
