using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    public static float FindAvarageRotaton(float a, float b)
    {
        if (a < 0) return FindAvarageRotaton(a + 360, b);
        if (b < 0) return FindAvarageRotaton(a, b + 360);
        if (a < b) return FindAvarageRotaton(b, a);
        if ((a - b) > 180) a -= 360;
        return (a + b) / 2;
    }

    public static float FindTheDegree(Vector2 a)
    {
        float m = a.magnitude;
        a /= m;
        float c = Mathf.Acos(a.x);
        float s = Mathf.Asin(a.y);
        if (c > Mathf.PI / 2)
        return (-(Mathf.PI/2 -s) / Mathf.PI) * 180;
        else return ((Mathf.PI / 2 - s) / Mathf.PI) * 180;
    }
}
