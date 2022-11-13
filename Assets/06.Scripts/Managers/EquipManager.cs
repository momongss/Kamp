using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager I { get; private set; }

    private void Awake()
    {
        I = this;
    }
}
