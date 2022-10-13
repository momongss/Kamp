using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : Cuttable
{
    private void OnTriggerEnter(Collider other)
    {
        print("Ãæµ¹");
        print(isCutCompleted);

        if (isCutCompleted && other.CompareTag(Tag.Pot))
        {
            print("µé¿È.");
            CookManager.Instance.OnCompleteWork(foodType, Food.Action.PutInPot);
        }
    }
}
