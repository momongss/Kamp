using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSet : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
