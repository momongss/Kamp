using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICookStep : MonoBehaviour
{
    public Food.Type type;
    public Food.Action action;

    [Range(1, 10)]
    public int totalStepCount;
    public int stepCount;

    public Cookware myCookware;

    public void StartStep()
    {
        stepCount = 0;

        if (myCookware)
        {
            myCookware.AllocateWork(type, totalStepCount);
        }
    }
}
