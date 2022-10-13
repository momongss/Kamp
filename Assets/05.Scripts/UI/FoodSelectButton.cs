using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSelectButton : MonoBehaviour
{
    public Food.Type foodType;
    public int cookCount = 1;

    public void Select()
    {
        CookManager.Instance.SelectRecipe(foodType, cookCount);
    }
}
