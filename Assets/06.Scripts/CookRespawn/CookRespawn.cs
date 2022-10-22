using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookRespawn : MonoBehaviour
{
    RespawnItem[] itemList;

    private void Awake()
    {
        Transform[] childs = Utils.GetChildren(transform);
        itemList = new RespawnItem[childs.Length];

        for (int i = 0; i < childs.Length; i++)
        {
            itemList[i] = childs[i].GetComponent<RespawnItem>();
        }
    }

    public void Respawn()
    {
        foreach (var prop in itemList)
        {
            prop.Respawn();
        }
    }
}
