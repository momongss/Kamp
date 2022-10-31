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
        // Define my target position in front of the camera ->
        Vector3 targetPosition = target.TransformPoint(new Vector3(offset.x, offset.y, CameraDistance));

        // Smoothly move my object towards that position ->
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // version 1: my object's rotation is always facing to camera with no dampening  ->
        transform.LookAt(target);
        // transform.LookAt(transform.position + target.rotation * Vector3.forward, target.rotation * Vector3.up);

        // version 2 : my object's rotation isn't finished synchronously with the position smooth.damp ->
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, 35 * Time.deltaTime);
    }
}
