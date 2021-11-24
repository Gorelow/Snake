using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }
    [SerializeField]
    public int GOPAmount;
    public float GOPRange;
    public float GOPMinDist;

    public float EGRotationSpeed;
    public float EGHoveringSpeed;
    public float EGHoverMax;
    public float EGHoverMin;

    public float SnakeSpeed;
    public float SnakeSpeedX;
    public float SnakeTailDelay;

    public int LevelNumber;
    public string LevelName;

    public void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Another instance of GameSettings already exists!");
        }
        Instance = this;

        Colorer.Set();

        GroupOfPeople.Amount = GOPAmount;
        GroupOfPeople.Range = GOPRange;
        GroupOfPeople.MinDist = GOPMinDist;

        EntitiesGlobal.RotationSpeed = EGRotationSpeed;
        EntitiesGlobal.HoveringSpeed = EGHoveringSpeed;
        EntitiesGlobal.HoverMax = EGHoverMax;
        EntitiesGlobal.HoverMin = EGHoverMin;

        Snake.Speed = SnakeSpeed;
        Snake.SpeedX = SnakeSpeedX;

        Level.Number = LevelNumber;
        Level.Name = LevelName;
    }
}
