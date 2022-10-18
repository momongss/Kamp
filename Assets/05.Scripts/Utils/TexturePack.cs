using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePack : MonoBehaviour
{
    public static TexturePack Instance;

    public Texture meatDemoCooked;

    private void Awake()
    {
        Instance = this;
    }
}
