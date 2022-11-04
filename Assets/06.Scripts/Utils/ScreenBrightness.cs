using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBrightness : MonoBehaviour
{
    public float brightness = 1f;

    // Update is called once per frame
    void Update()
    {
        Screen.brightness = brightness;
    }
}
