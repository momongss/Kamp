using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipButton : MonoBehaviour
{
    public Item.Type type;
    public TextMeshProUGUI Text_itemCount;

    public SpawnBall spawnBall;

    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (SpawnItemBall())
            {
                UIEquipment.I.CloseNotice();
            }
        });
    }

    public void SetItemCount(int count)
    {
        Text_itemCount.text = $"{count}";
    }

    public bool SpawnItemBall()
    {
        int itemCount = ItemManager.I.GetItemCount(type);
        if (itemCount == 0) return false;

        Instantiate(spawnBall, Player.I.spawnBallPoint.position, Player.I.spawnBallPoint.rotation);

        ItemManager.I.UseItem(type);

        SetItemCount(itemCount - 1);

        return true;
    }
}
