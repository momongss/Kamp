using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform mainCam;

    private void Start()
    {
        mainCam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(mainCam);
    }
}