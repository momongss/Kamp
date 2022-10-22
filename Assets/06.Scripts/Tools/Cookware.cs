using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookware : MonoBehaviour
{
    Food.Type currWorkingFoodType;
    int totalWorkCount;
    int workCount;

    public void AllocateWork(Food.Type foodType, int _workCount)
    {
        currWorkingFoodType = foodType;
        totalWorkCount = _workCount;
        workCount = 0;
    }
}
