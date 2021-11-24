using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefabs 
{
    public static EntityView Human = Resources.Load<EntityView>("Human");
    public static EntityView Crystal = Resources.Load<EntityView>("Crystal");
    public static EntityView Obstacle = Resources.Load<EntityView>("Obstacle");

    public static GroupOfPeople GroupOfPeople = Resources.Load<GroupOfPeople>("GroupOfPeople");
    public static ColoringLine RecolorLine = Resources.Load<ColoringLine>("RecolorLine");
    public static SegmentController segment = Resources.Load<SegmentController>("Segment");

    public static Material[] Colors = Resources.LoadAll<Material>("Materials");
    public static Material MaterialBase = Resources.Load<Material>("Materials/Red");

    public static Sprite CrystalSprite = Resources.Load<Sprite>("Points/Crystal");
    public static Sprite HumanSprite = Resources.Load<Sprite>("Points/Human");

    public static GameObject PlusOne = Resources.Load<GameObject>("+1");

    public static List<string> AllAvalibleItems = new List<string>()
    {
        "Crystal", "Human"
    };

    public static Sprite ItemSprite(string Name)
    {
        if (!AllAvalibleItems.Contains(Name)) return null;
        return Resources.Load<Sprite>($"Items/{Name}");
    }
}
