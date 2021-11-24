using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScreens : MonoBehaviour
{
    public static EndingScreens Instance { get; private set; }
    public GameObject WinScreen;
    public GameObject LooseScreen;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Another instance of EndingScreens already exists!");
        }
        Instance = this;
    }

    public void End(bool win)
    {
        if (win) WinScreen.SetActive(true);
        else LooseScreen.SetActive(true);
    }
}
