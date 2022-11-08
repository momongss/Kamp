using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type
    {
        Tent01 = 1,
        Tent02 = 2,
        Tent03 = 3,
        Tent04 = 4,
        Tent05 = 5,

        Mat01 = 6,
    }

    public Type type;
}
