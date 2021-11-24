using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    public Text LevelName;

    public Animator FeverVisualisation;

    public ViewImageValue PeopleView;
    public ViewImageValue CrystalsView;

    public void SetActive(bool active)
    {
        PeopleView.SetActive(active);
        CrystalsView.SetActive(active);
        SetLevelName(Level.Number, Level.Name);
    }
    
    public void SetLevelName(int num,string name)
    {
        LevelName.text = $"Level {num} - {name}";
    }

    public void SetPeople(int val)
    {
        PeopleView.ChangeValue(val);
    }

    public void SetCrystals(int val)
    {
        CrystalsView.ChangeValue(val);
    }

    public void SetFever(bool active)
    {
        if (active) FeverVisualisation.Play("Activate");
        else FeverVisualisation.Play("Deactivate");
    }
}
