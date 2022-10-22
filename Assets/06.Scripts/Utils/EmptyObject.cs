using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyObject : MonoBehaviour
{
    public static EmptyObject I;

    private void Awake()
    {
        I = this;
    }
}
