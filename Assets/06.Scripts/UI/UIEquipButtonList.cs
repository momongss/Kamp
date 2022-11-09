using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipButtonList : MonoBehaviour
{
    UIEquipButton[] uIEquipButtons;

    private void Awake()
    {
        uIEquipButtons = GetComponentsInChildren<UIEquipButton>(true);

        foreach (var b in uIEquipButtons)
        {
            int itemCount = ItemManager.I.GetItemCount(b.type);
            if (itemCount > 0)
            {
                b.gameObject.SetActive(true);
                b.SetItemCount(itemCount);
            }
            else
            {
                b.gameObject.SetActive(false);
            }
        }
    }
}
