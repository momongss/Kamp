using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OpeningCameraMove : MonoBehaviour
{
    public CinemachineVirtualCamera[] vcams;

    void Start()
    {
        SetCamera(0);

        StartCoroutine(MovingRoutine());
    }

    void SetCamera(int camNum)
    {
        for (int i = 0; i < vcams.Length; i++)
        {
            var cam = vcams[i];
            cam.Priority = 1;
        }

        vcams[camNum].Priority = 2;
    }

    IEnumerator MovingRoutine()
    {
        for (int i = 0; i < vcams.Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            SetCamera(i);
        }
    }
}
