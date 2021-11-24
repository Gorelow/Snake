using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : MonoBehaviour
{
    public SnakePart Next;
    public MeshRenderer M;
    public Animation Eating;

    private Vector3 trail; 
    private float MaxLength;
    private float rotation; 

    public void Recolor(int group)
    {
        Colorer.Recolor(M,group);
        var t = TimerController.Instance;
        if (t && (Next != null))
            new SpecialTimer<int>(group).SetTheTimer(GameSettings.Instance.SnakeTailDelay) += Next.Recolor;
    }

    public void Eat()
    {
        if (Eating!= null)  Eating?.Play();
        var t = TimerController.Instance;
        if (t && (Next != null))
                t.SetTheTimer(GameSettings.Instance.SnakeTailDelay) += Next.Eat;
    }

    public void Follow(Vector3 vector, bool debug = false)
    {
        Vector2 diff = new Vector2(vector.x - trail.x, vector.z - trail.z);
        rotation = Calculator.FindTheDegree(diff);
        transform.eulerAngles = Vector3.up * rotation;
        if (MaxLength < diff.magnitude)
        {
            trail += (new Vector3(diff.x, 0, diff.y) / diff.magnitude) * (diff.magnitude - MaxLength);
        }
        transform.position = trail;
        if (Next == null) return;
        Next.Follow(trail);
    }
    
    public void Set(Vector3 pos)
    {
        trail = transform.position;
        MaxLength = (trail - pos).magnitude;
        if (Next == null) return;
        Next.Set(trail);
    }
}