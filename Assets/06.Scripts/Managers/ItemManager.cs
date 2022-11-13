using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    public static ItemManager I { get; private set; }

    // 현재 아이템 종류의 최대 갯수는 100개.
    ItemDataList itemDataList;

    const string item_dataPath = "/Item.json";

    Dictionary<Item.Type, int> usingItemCount_Dic = new Dictionary<Item.Type, int>();

    private void Awake()
    {
        I = this;
        if (JsonData.isFileExist(item_dataPath) == false)
        {
            JsonData.SaveObj(new ItemDataList(), item_dataPath);
        }

        string s_item = JsonData.LoadJson(item_dataPath);
        itemDataList = JsonUtility.FromJson<ItemDataList>(s_item);

        foreach (ItemData d in itemDataList.itemCountList)
        {
            usingItemCount_Dic.Add(d.type, 0);
        }
    }

    public int GetItemCount(Item.Type type)
    {
        ItemData item = GetItem(type);

        if (item == null) return 0;
        else return item.count - usingItemCount_Dic[type];
    }

    public void UseItem(Item.Type type, bool isDisposable = false)
    {
        if (isDisposable)
        {
            ItemData d = GetItem(type);

            if (d.count <= 0)
            {
                Debug.LogError($"로직에러 위험 : 개수가 0인 아이템 사용 발생 {d}");
                return;
            }

            d.count--;
            JsonData.SaveObj(itemDataList, item_dataPath);
        }
        else
        {
            usingItemCount_Dic[type]++;
        }
    }

    ItemData GetItem(Item.Type type)
    {
        return itemDataList.itemCountList.Find(item => item.type == type);
    }

    public void BuyItem(Item.Type type)
    {
        ItemData item = GetItem(type);
        if (item == null)
        {
            itemDataList.itemCountList.Add(new ItemData(type, 1));
        }
        else
        {
            item.count++;
        }

        JsonData.SaveObj(itemDataList, item_dataPath);
    }

    public void SellItemCount(Item.Type type)
    {

    }
}

public class ItemDataList
{
    public List<ItemData> itemCountList = new List<ItemData>();
}

[Serializable]
public class ItemData
{
    public Item.Type type;
    public int count = 0;

    public ItemData(Item.Type _type, int _count)
    {
        type = _type;
        count = _count;
    }
}