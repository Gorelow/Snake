using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public SnakeController snake;

    public Vector3 relative_position;


    public void Activate()
    {
        relative_position = snake.Pos - transform.position;
        TimerController.Instance.OnTick += Follow;
    }

    public void Follow()
    {
        transform.position += (-transform.position.z * 0.4f + (snake.Pos - relative_position).z * 0.4f) * Vector3.forward;
    }
}
