using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    public enum State { Select, Cooking, CompleteCooking };

    public State state;

    public static CookManager Instance;

    public UICookStepManager uICookStep;
    public UISelectRecipe uISelectRecipe;
    public UIPanel uIComplete;

    public void SelectRecipe(Food.Type foodType, int count = 1)
    {
        uISelectRecipe.Hide();
        uICookStep.StartCookStep(foodType, count);
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
                uICookStep.Hide();
                uIComplete.Hide();
                break;
            case State.Cooking:
                uISelectRecipe.Hide();
                uICookStep.Show();
                uIComplete.Hide();
                break;
            case State.CompleteCooking:
                uISelectRecipe.Hide();
                uICookStep.Hide();
                uIComplete.Show();
                break;
        }
    }

    public void OnCompleteWork(Food.Type type, Food.Action action)
    {
        uICookStep.OnCompleteWork(type, action);
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
