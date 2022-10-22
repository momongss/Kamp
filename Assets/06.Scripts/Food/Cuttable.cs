using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : Food
{
    int requiredCutCount;
    int currCutCount = 0;

    protected bool isCutCompleted = false;

    private void Awake()
    {
        requiredCutCount = transform.GetChild(0).childCount - 1;
    }
}
