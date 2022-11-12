using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    public GameObject item;

    public GameObject prevSpawnedItem;

    public void Respawn()
    {
        if (prevSpawnedItem)
        {
            Destroy(prevSpawnedItem);
        }
        prevSpawnedItem = Instantiate(item, transform.position, transform.rotation);
    }
}
