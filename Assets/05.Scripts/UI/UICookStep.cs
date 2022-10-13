using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICookStep : MonoBehaviour
{
    public Food.Type type;
    public Food.Action action;

    public int totalStepCount;
    public int stepCount;

    public void SetTotalStepCount(int count)
    {
        totalStepCount = count;
        stepCount = 0;
    }
}
