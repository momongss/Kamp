using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static bool isTargetOnLeft(Transform origin, Transform target)
    {
        return false;
    }

    public static Collider RayCast(Vector3 origin, Vector3 dir, int layerMask = ~0)
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(origin, dir, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(origin, dir * hit.distance, Color.yellow);

            return hit.collider;
        }
        else
        {
            Debug.DrawRay(origin, dir * 1000, Color.white);

            return null;
        }
    }

    public static Transform[] GetChildren(Transform parent)
    {
        Transform[] children = new Transform[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
        {
            children[i] = parent.GetChild(i);
        }

        return children;
    }
}
