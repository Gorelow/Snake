using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityType type;
    public int Group;
}

public enum EntityType
{
    Recolor_line,
    Crystal,
    Human,
    Obstacle
}