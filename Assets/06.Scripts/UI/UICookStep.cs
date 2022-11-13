using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICookStep : MonoBehaviour
{
    public Food.Type type;
    public Food.Action action;

    [Range(1, 10)]
    public int totalWorkCount;
    public int workCount;

    public RespawnItem[] respawnItems;

    public void StartStep()
    {
        workCount = 0;

        gameObject.SetActive(true);
        RespawnItem();
    }

    public void RespawnItem()
    {
        respawnItems = GetComponentsInChildren<RespawnItem>();
        foreach (var item in respawnItems)
        {
            print(item);
            item.Respawn();
        }
    }
}
