using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public static Track Instance { get; private set; }
    public float Width;
    public float Length;
    public int SegmAmount;
    public SnakeController Snake;

    public GameObject Road;

    private int _loaded = 0;
    private float _length = 10;

    private List<SegmentController> activeSegments;


    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Another instance of Track already exists!");
        }
        Instance = this;
    }

    void Start()
    {
        activeSegments = new List<SegmentController>();
        Load(2);
        TimerController.Instance.OnTick += Step;
    }
    
    public void Step()
    {
        Road.transform.position = Snake.Pos.z * Vector3.forward;
        if (Snake.Pos.z > _length - 20)
        {
            Clear(1);
            Load(1);
        }
        if (activeSegments.Count == 0)
        LevelController.EndGame(true);
    }

    public void Load(int segments)
    {
        if (_loaded >= SegmAmount) { _length += 20; return; }
        int[,] TEST = new int[,]
        {
            { 1, -1, -1, 1 },
            { -1, 1, -1, -1 },
            { 1, -1, -1, 1 }
        };

        int[][,] TEST2 = new int[][,]{ 
            new int[,]{
            { 0, 1, -1, -1, 1 },
            { 0, -1, 1, 1, -1} },
             new int[,]{
            { 0, -1, 1, -1, 1 },
            { 0, -1, 1, 1, 0} },
              new int[,]{
            { 0, -1, 1, -1, 1 },
            { 0, 1, -1, 0, -1} }
        };
        for (int i = _loaded; i < _loaded + segments; i++)
        {
            SegmentController segment = Instantiate(Prefabs.segment, transform);
            if (i % 2 == 1)
                segment.Set(TEST, Prefabs.Crystal, Prefabs.Obstacle);
            else
                segment.Set(TEST2[Random.Range(0,3)], Prefabs.GroupOfPeople, Prefabs.RecolorLine);
            segment.transform.localPosition = Vector3.forward * _length;
            _length += segment.model.length;
            activeSegments.Add(segment);
        }
        _loaded += segments;
    }

    public void Clear(int segment)
    {
        for (int i = 0; i < segment; i++)
        {
            Destroy(activeSegments[0].gameObject, 0.1f);
            activeSegments.RemoveAt(0);
        }
    }
}
