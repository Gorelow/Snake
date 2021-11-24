using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Colorer
{
    public static List<Color> Colors = new List<Color>()
    {
        new Color(1,0,0),
        //new Color(1,0.5f,0),
        new Color(1,1,0),
        //new Color(0.5f,1,0),

        new Color(0,1,0),
        //new Color(0,1,0.5f),
        new Color(0,1,1),
        //new Color(0,0.5f,1),

        new Color(0,0,1),
        //new Color(0.5f,0,1),
        new Color(1,0,1)//,
        //new Color(1,0,0.5f)
    };

    public static List<Material> Materials = new List<Material>();

    public static void Set()
    {
        for (int i = 0;i<Colors.Count;i++)
        {
            Materials.Add(new Material(Prefabs.MaterialBase));
            Materials[i].color = Colors[i];
        }
    }

    public static Color GetColor(int group)
    {
        if (group > Colors.Count) return Color.white;
        return Colors[group];
    }

    public static void Recolor(MeshRenderer mesh,int group)
    {
        if (mesh != null)
        mesh.material = Materials[group];
    }
}
