using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public bool isSeated { get; private set; }

    public Transform seatCenter;

    private void Awake()
    {
        isSeated = false;
    }

    public void Sit(Transform t)
    {
        t.position = transform.position;
        t.LookAt(PlaceEattingZone.I.transform);

        isSeated = true;
    }

    public void UnSit()
    {
        isSeated = false;
    }
}
