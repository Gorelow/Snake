using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityView : MonoBehaviour
{
    public Entity model;
    public bool Rotating;
    public bool Hovering;

    public Collider Collider;

    private Vector3 StPos;
    private Vector3 StRot;
    private int baseRotation;

    public bool Active;
    public bool Killed = false;
    public bool EEaten = false;

    public void Start()
    {
        SetActive(true);
    }

    public void Rotate()
    {
        transform.eulerAngles = StRot + EntitiesGlobal.Rotation * Vector3.forward;
    }

    public void Hover()
    {
        transform.position = StPos + EntitiesGlobal.Y * Vector3.up;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (EEaten) Debug.Log("I was eaten twice");
        EEaten = true;
        Collider.enabled = false;
        if (other.tag != "Player") return;
        SetActive(false);
        TimerController.Instance.OnTick += Eaten;
        TimerController.Instance.SetTheTimer(0.3f) += Kill;
    }

    public void Eaten()
    {
        transform.position = SnakeController.Instance.Pos*0.3f+ transform.position*0.7f;
        transform.localScale *= 0.8f;
    }

    public void Kill()
    {
        if (this == null) return;
        if (Killed) return;
        Killed = true;
        TimerController.Instance.OnTick -= Eaten;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SetActive(false);
        var t = TimerController.Instance;
        if (t != null)
        {
            t.OnTick -= Rotate;
            t.OnTick -= Hover;
            t.OnTick -= Eaten;
        }
    }


    public void SetActive(bool active)
    {
        if (Active == active) return;
        var t = TimerController.Instance;
        if (t != null)
            if (active)
            {
                if (Rotating)
                { 
                    t.OnTick += Rotate;
                    StRot = transform.eulerAngles;
                }
                if (Hovering)
                {
                    t.OnTick += Hover;
                    StPos = transform.position;
                }
            }
            else
            {
                if (Rotating)
                { 
                    t.OnTick -= Rotate;
                    transform.eulerAngles = StRot;
                }
                if (Hovering)
                {
                    t.OnTick -= Hover;
                    transform.position = StPos;
                }
            }
        Active = active;
    }
}
