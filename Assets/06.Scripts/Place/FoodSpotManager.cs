using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpotManager : MonoBehaviour
{
    public static FoodSpotManager I { get; private set; }

    public FoodSpot[] foodSpotList;

    public int foodIndex = 0;

    private void Awake()
    {
        I = this;

        Transform[] T_foodSpotList = Utils.GetChildren(transform);

        foodSpotList = new FoodSpot[T_foodSpotList.Length];

        for (int i = 0; i < foodSpotList.Length; i++)
        {
            foodSpotList[i] = T_foodSpotList[i].GetComponent<FoodSpot>();
        }
    }

    public void PutFood(Food food)
    {
        FoodSpot foodSpot = GetEmptyFoodSpot();

        foodSpot.AssignFood(food);
    }

    FoodSpot GetEmptyFoodSpot()
    {
        FoodSpot spot = foodSpotList[foodIndex];
        foodIndex++;
        if (foodIndex >= foodSpotList.Length)
        {
            foodIndex = 0;
        }

        return spot;
    }
}