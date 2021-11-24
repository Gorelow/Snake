using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesGlobal : MonoBehaviour
{
    public static float RotationSpeed;

    public static float HoveringSpeed;
    public static float HoverMax;
    public static float HoverMin;

    public static float Rotation;
    public static float Y;
    
    private int direction = 1;

    private bool Active = false;

    public void Start()
    {
        SetActive(true);
    }

    public void SetActive(bool active)
    {
        if (Active == active) return;
        var t = TimerController.Instance;
        if (t != null)
            if (active)
            {
                t.OnTick += Rotate;
                t.OnTick += Hover;
            }
            else
            {
                t.OnTick -= Rotate;
                t.OnTick -= Hover;
            }
        Active = active;
    }

    public void Rotate()
    {
        Rotation += RotationSpeed;
    }

    public void Hover()
    {
        Y += direction * HoveringSpeed;
        if (Y > HoverMax) direction = -1;
        if (Y < HoverMin) direction = 1;
    }

    public void OnDestroy()
    {
        SetActive(false);
    }
}
