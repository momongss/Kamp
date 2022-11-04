using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpotManager : MonoBehaviour
{
    public static FoodSpotManager I { get; private set; }

    public FoodSpot[] foodSpotList;

    private void Awake()
    {
        I = this;

        Transform[] T_foodSpotList = Utils.GetChildren(transform);

        foodSpotList = new FoodSpot[T_foodSpotList.Length];

        for (int i = 0; i < foodSpotList.Length; i++)
        {
            foodSpotList[i] = T_foodSpotList[i].GetComponent<FoodSpot>();
        }

        print(foodSpotList.Length);
    }

    public void PutFood(Food food)
    {
        FoodSpot foodSpot = GetEmptyFoodSpot();

        if (foodSpot == null)
        {
            return;
        }

        foodSpot.AssignFood(food);
    }

    FoodSpot GetEmptyFoodSpot()
    {
        foreach (FoodSpot f in foodSpotList)
        {
            if (f.isPlated == false)
            {
                return f;
            }
        }

        return null;
    }
}