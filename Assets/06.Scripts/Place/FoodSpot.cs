using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpot : MonoBehaviour
{
    public Food food { get; private set; }

    public bool isPlated = false;

    public void AssignFood(Food _food)
    {
        isPlated = true;

        food = _food;
        food.transform.parent = transform;
        food.transform.localPosition = Vector3.zero;
    }

    public void CleanSpot()
    {
        isPlated = false;

        if (food)
        {
            food.Destroy();
            food = null;
        }
    }
}
