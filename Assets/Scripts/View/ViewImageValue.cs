using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewImageValue : MonoBehaviour
{
    public GameObject main;
    public Image Image;
    public Text Value;
    public Animation AdditionEffect;
    public string Name;

    public void SetActive(bool active)
    {
        main.SetActive(active);
        if (active)
        {
            Image.sprite = Prefabs.ItemSprite(Name);
            Value.text = 0.ToString();
        }
    }

    public void ChangeValue(int val, bool addEffect = true)
    {
        if (addEffect) AdditionEffect?.Play();
        Value.text = val.ToString();
    }
}
