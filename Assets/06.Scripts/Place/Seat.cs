using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public bool isSeated { get; private set; }

    private void Awake()
    {
        isSeated = false;
    }

    public void Sit()
    {
        isSeated = true;
    }

    public void UnSit()
    {
        isSeated = false;
    }
}
