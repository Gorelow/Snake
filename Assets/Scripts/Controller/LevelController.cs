using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static SnakeController Snake;
    public MainCamera Camera;
    public LevelView view;
    public Level model;

    public GameObject Finish;

    public void Start()
    {
        SetActive(true);
    }

    public void SetActive(bool active)
    {
        Snake = SnakeController.Instance;
        Snake.Activate();
        Camera.Activate();
        view.SetActive(active);
        model.OnCrystalsChange += view.SetCrystals;
        model.OnPeopleChange += view.SetPeople;
        Snake.OnEatCrystals += CrystalEaten;
        Snake.OnEatPeople += HumanEaten;
        Snake.OnFever += view.SetFever;
    }

    public void CrystalEaten()
    {
        model.EatCrystal();
        var snake = SnakeController.Instance;
        if (!snake.model.Invinsible)
        {
            snake.ChangeFeverPoint(true);
            new SpecialTimer<bool>(false).SetTheTimer(2f) += snake.ChangeFeverPoint;
        }
    }

    public void HumanEaten(int group)
    {
        model.EatPeople();
        if (Snake.model.Invinsible) return;
        if (group == Snake.model.Group) return;
        EndGame(false);
    }

    public static void EndGame(bool win)
    {
        Snake.SetActive(false);
        EndingScreens.Instance.End(win);
    }
}
