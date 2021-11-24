using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public int Length;
    public int Crystals;
    public bool Fever { get; private set; }
    public bool Invinsible { get; private set; }
    public int FeverPoints;
    public static float Speed;
    public static float SpeedX;
    public float Group;

    public void SetFever(bool active)
    {
        if (active)
            Speed *= 3;
        else Speed /= 3;
        Fever = active;
    }

    public void SetInvinsibility(bool active)
    {
        Invinsible = active;
    }
}
