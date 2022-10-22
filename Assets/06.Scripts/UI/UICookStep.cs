using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICookStep : MonoBehaviour
{
    public Food.Type type;
    public Food.Action action;

    [Range(1, 10)]
    public int totalWorkCount;
    public int workCount;

    public void StartStep()
    {
        workCount = 0;
    }
}
