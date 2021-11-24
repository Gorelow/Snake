using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Level : MonoBehaviour
{
    public static int Number;
    public static string Name;

    public static int Length;
    public static int Segments;

    public int Crystals;
    public int People;

    public Action<int> OnCrystalsChange;
    public Action<int> OnPeopleChange;

    public void EatCrystal()
    {
        Crystals++;
        OnCrystalsChange.Invoke(Crystals);
    }

    public void EatPeople()
    {
        People++;
        OnPeopleChange.Invoke(People);
    }
}
