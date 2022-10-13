using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour
{
    public Food.Type foodType;

    int requiredCutCount;
    int currCutCount = 0;

    protected bool isCutCompleted = false;

    private void Awake()
    {
        requiredCutCount = transform.GetChild(0).childCount - 1;
    }

    public void Cut()
    {
        currCutCount++;

        if (currCutCount >= requiredCutCount)
        {
            OnCompleteCutting();
        }
    }

    void OnCompleteCutting()
    {
        isCutCompleted = true;
        CookManager.Instance.OnCompleteWork(foodType, Food.Action.Cut);
    }
}
