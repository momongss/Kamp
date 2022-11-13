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

        var PS_Smoke = Instantiate(EffectPack.I.PS_Smoke, transform.position, transform.rotation);
        PS_Smoke.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        PS_Smoke.Play();
        Destroy(PS_Smoke.gameObject, 4f);
    }
}
