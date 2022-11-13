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
            c.ChangeState(Character.State.GotoEat);
        }
    }

    public void OnCompleteCook(Food food)
    {

    }
}
