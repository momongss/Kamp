using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    public GameObject item;

    public void Respawn()
    {
        print("Spawn");
        Instantiate(item, transform.position, Quaternion.identity);
    }
}
