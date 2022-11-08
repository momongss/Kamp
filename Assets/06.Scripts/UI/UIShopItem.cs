using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShopItem : MonoBehaviour
{
    public Item.Type itemType;
    public TextMeshProUGUI Text_itemCount;
    public TextMeshProUGUI Text_itemPrice;

    public int price = 100;

    private void Start()
    {
        int itemCount = ItemManager.I.GetItemCount(itemType);
        Text_itemCount.text = $"{itemCount}";
        Text_itemPrice.text = $"{price}¿ø";
    }

    public void BuyItem()
    {
        ItemManager.I.BuyItem(itemType);

        bool buySuccess = MoneyManager.I.UseMoney(price);

        if (buySuccess)
        {
            int itemCount = ItemManager.I.GetItemCount(itemType);
            Text_itemCount.text = $"{itemCount}";
        }
    }
}
