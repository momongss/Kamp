using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : Cuttable
{
    private void OnTriggerEnter(Collider other)
    {
        print("�浹");
        print(isCutCompleted);

        if (isCutCompleted && other.CompareTag(Tag.Pot))
        {
            print("���.");
            CookManager.Instance.OnCompleteWork(foodType, Food.Action.PutInPot);
        }
    }
}
