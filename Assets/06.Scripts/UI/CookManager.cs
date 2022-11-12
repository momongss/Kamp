using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    public enum State { Select, Cooking, CompleteCooking };

    public State state;

    public static CookManager I;

    public UICookStepManager uICookStepManager;
    public UISelectRecipe uISelectRecipe;
    public UIPanel uIComplete;

    public List<Food> food_completed_list = new List<Food>();

    [SerializeField] CookingSet cookingSet;

    public void SelectRecipe(Food.Type foodType, int count = 1)
    {
        uISelectRecipe.Hide();
        uICookStepManager.StartCookStep(foodType, count);
        // CookRespawnManager.I.Respawn(foodType, count);
    }

    public void StartCooking()
    {
        cookingSet.Activate();
        MealManager.I.StartMeal();
    }

    private void Awake()
    {
        I = this;

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

    public void OnCompleteCooking(Food food)
    {
        SetState(State.CompleteCooking);

        MissionManager.I.OnMissionComplete(MissionManager.Type.Cook);

        print($"Add {food}");
        food_completed_list.Add(food);
    }

    public void ShowSelectPanel()
    {
        SetState(State.Select);
    }
}
