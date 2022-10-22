using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : Cuttable
{
    private void OnTriggerEnter(Collider other)
    {
        if (isCutCompleted && other.CompareTag(Tag.Pot))
        {
            CookManager.Instance.OnCompleteWork(this, Food.Action.PutInPot);
        }
    }
}
