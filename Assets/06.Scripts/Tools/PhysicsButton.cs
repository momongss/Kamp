using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    public UnityEvent OnClick;

    private void OnTriggerEnter(Collider other)
    {
        print("Enter");
        if (other.CompareTag(Tag.PhyicsButton))
        {
            print("hit");
            OnClick.Invoke();
        }
    }
}
