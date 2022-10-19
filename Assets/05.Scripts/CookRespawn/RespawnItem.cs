using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    public GameObject item;
    public Collider coll;

    public void Respawn()
    {
        coll.enabled = false;

        Instantiate(item, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
