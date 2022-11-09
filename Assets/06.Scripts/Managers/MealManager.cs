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
        foreach (Character c in CampingManager.Instance.activeCharacterList)
        {
            c.GotoEat();
        }

        Seat seat = PlaceEattingZone.I.GetSeat();
        seat.Sit(Player.I.transform);

        List<Food> foodList = CookManager.Instance.food_completed_list;

        foreach (Food f in foodList)
        {
            FoodSpotManager.I.PutFood(f);
        }
    }

    public void OnCompleteCook(Food food)
    {

    }
}
