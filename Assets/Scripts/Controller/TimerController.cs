using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance { get; private set; }
    public int CurrentTime;
    public float Speed;

    private float _time;

    public Action OnTick;

    public List<Timer> Timers;
    public List<ITimer> specialTimers;

    void Awake()
    {
        Timers = new List<Timer>();
        specialTimers = new List<ITimer>();
        if (Instance != null)
        {
            Debug.LogError("Another instance of TimerController already exists!");
        }
        Instance = this;
    }
    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void Update()
    {
        if (Speed <= 0) { 
            return; }
        _time += Time.deltaTime;
        if (_time > (1 / Speed)) Tick();
    }

    public void Tick()
    {
        OnTick?.Invoke();
        CurrentTime++;
        _time -= 1 / Speed;
        CheckTimers();
        if (_time > (1 / Speed)) Tick();
    }

    public void Pause(bool pause)
    {
        if (pause) Speed = 0;
        else Speed = 120;
    }

    public ref Action SetTheTimer(float time)
    {
        var t = new Timer(time + CurrentTime/Speed);
        Timers.Add(t);
        return ref t.OnActivation;
    }

    public void CheckTimers()
    {
        if (Speed <= 0) return;
        for (int i = 0; i < Timers.Count; i++)
            if (Timers[i].end <= CurrentTime / Speed)
            {
                Timers[i].OnActivation?.Invoke();
                Timers.Remove(Timers[i]);
                i--;
            }
        for (int i = 0; i < specialTimers.Count; i++)
            if (specialTimers[i].End <= CurrentTime/Speed)
            {
                specialTimers[i].TimesUp();
                specialTimers.Remove(specialTimers[i]);
                i--;
            }
    }
}

public interface ITimer
{
    float End { get; }
    bool Check(int t);
    void TimesUp();
}
public class Timer
{
    public float end;
    public Action OnActivation;
    public Timer(float t)
    {
        end = t;
    }
}

public class SpecialTimer<T> : Timer, ITimer
{
    public float End { get { return end; } }
    public T value;
    public Action<T> OnActivationT;
    public SpecialTimer(T val) : base(TimerController.Instance.CurrentTime)
    {
        value = val;
    }

    public ref Action<T> SetTheTimer(float t)
    {
        var timer = TimerController.Instance;
        end = t + timer.CurrentTime / timer.Speed;
        var list = timer.specialTimers;
        list.Add(this);
        return ref this.OnActivationT;
    }

    public void TimesUp()
    {
        OnActivationT?.Invoke(value);
    }

    public bool Check(int t)
    {
        return true;
    }
}
