using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCampZone : MonoBehaviour
{
    public static PlaceCampZone Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
}
