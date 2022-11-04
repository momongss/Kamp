using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stew : Food
{
    public Transform T_foodList;
    public List<Food> foodList = new List<Food>();

    public void PutFood(Food _food)
    {
        foodList.Add(_food);
        _food.transform.parent = T_foodList;
    }
}
