using System.Collections.Generic;
using System;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public static SnakeController Instance;

    public Snake model;
    public SnakePart snakePart;
    public bool Active;

    public Vector3 Pos;
    private float AimPos;
    private float MaxPos;

    public Action<int> OnEatPeople;
    public Action OnEatCrystals;
    public Action<bool> OnFever;

    private Vector3 trail;
    public Canvas PlusOne;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Another instance of SnakeController already exists!");
        }
        Instance = this;
    }

    public void Activate()
    {
        Pos = transform.localPosition;   
        MaxPos = Track.Instance.Width / 2;
        SetActive(true);
        snakePart.Set(transform.position);
    }

    private void OnMouseDrag()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            SetPos(hit.point.x);
        }
    }

    public void SetPos(float p)
    {
        AimPos = Mathf.Max(Mathf.Min(MaxPos, p),-MaxPos);
    }

    public void Step()
    {
        if (model.Fever) SetPos(0);
        Pos.x = Pos.x * (1 - Snake.SpeedX) + AimPos * Snake.SpeedX;
        Pos += Vector3.forward * Snake.Speed;
        transform.localPosition = Pos;
        
        snakePart.Follow(transform.position, true);
        trail = Pos;
    }

    public void Eat(Entity entity)
    {
        Instantiate(Prefabs.PlusOne, PlusOne.transform);
        snakePart?.Eat();
        switch (entity.type)
        {
            case EntityType.Obstacle:
                if (!model.Invinsible)
                {
                    SetActive(false);
                    EndingScreens.Instance.End(false);
                }
                return;
            case EntityType.Human:
                OnEatPeople.Invoke(entity.Group);
                return;
            case EntityType.Crystal:
                OnEatCrystals.Invoke();
                return;
            default:  break;
        }

    }

    public void Recolor(int group)
    {
        snakePart?.Recolor(group);
        model.Group = group;
    }

    public void SetFever(bool val)
    {
        model.SetFever(val);
        OnFever?.Invoke(val);
        if (val)
        {
            SetInvinsible(true);
            new SpecialTimer<bool>(false).SetTheTimer(5f) += SetFever;
        }
    }

    public void SetInvinsible(bool val)
    {
        model.SetInvinsibility(val);
        if (val)
        {
            
            new SpecialTimer<bool>(false).SetTheTimer(7f) += SetInvinsible;
        }
    }

    public void ChangeFeverPoint(bool AddOrReduce)
    {
        model.FeverPoints += AddOrReduce ? 1 : -1;
        if (model.FeverPoints >= 3) SetFever(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity>())
        Eat(other.GetComponent<Entity>());
    }

    private void OnDestroy()
    {
        SetActive(false);
    }

    public void SetActive(bool active)
    {
        if (Active == active) return;
        var t = TimerController.Instance;
        if (t != null)
            if (active)
            {
                t.OnTick += Step;
            }
            else
            {
                t.OnTick -= Step;
            }
        Active = active;
    }
}
