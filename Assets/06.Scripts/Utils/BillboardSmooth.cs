using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSmooth : MonoBehaviour
{
    public float CameraDistance = 3.0F;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Transform target;

    public Vector3 offset;

    void Awake()
    {
        target = Camera.main.transform;
    }


    void Update()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(offset.x, offset.y, CameraDistance));

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(target);
    }
}
