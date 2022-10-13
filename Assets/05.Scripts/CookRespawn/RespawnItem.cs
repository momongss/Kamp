using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    public GameObject item;

    public void Respawn()
    {
        Instantiate(item, transform.position, Quaternion.identity, transform);
    }
}
