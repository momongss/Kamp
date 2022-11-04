using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MealManager : MonoBehaviour
{
    public static MealManager I { get; private set; }

    private void Awake()
    {
        I = this;
    }

    public void StartMeal()
    {
        print("식사 시작!");
        foreach (Character c in CampingManager.Instance.activeCharacterList)
        {
            c.GotoEat();
        }

        List<Food> foodList = CookManager.Instance.food_completed_list;

        print(foodList.Count);
        foreach (Food f in foodList)
        {
            FoodSpotManager.I.PutFood(f);
        }
    }

    public void OnCompleteCook(Food food)
    {

    }
}
