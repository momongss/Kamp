using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    public enum State { Select, Cooking, CompleteCooking };

    public State state;

    public static CookManager Instance;

    public UICookStepManager uICookStepManager;
    public UISelectRecipe uISelectRecipe;
    public UIPanel uIComplete;

    public void SelectRecipe(Food.Type foodType, int count = 1)
    {
        uISelectRecipe.Hide();
        uICookStepManager.StartCookStep(foodType, count);
        CookRespawnManager.I.Respawn(foodType, count);
    }

    private void Awake()
    {
        Instance = this;

        SetState(State.Select);
    }

    public void SetState(State _state)
    {
        state = _state;

        switch (state)
        {
            case State.Select:
                uISelectRecipe.Show();
                uICookStepManager.Hide();
                uIComplete.Hide();
                break;
            case State.Cooking:
                uISelectRecipe.Hide();
                uICookStepManager.Show();
                uIComplete.Hide();
                break;
            case State.CompleteCooking:
                uISelectRecipe.Hide();
                uICookStepManager.Hide();
                uIComplete.Show();
                break;
        }
    }

    public void OnCompleteWork(Food food, Food.Action action)
    {
        uICookStepManager.OnCompleteWork(food, action);
    }

    public void OnCompleteCooking()
    {
        SetState(State.CompleteCooking);
    }

    public void ShowSelectPanel()
    {
        SetState(State.Select);
    }
}
