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

    int itemCount = 0;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            SpawnItemBall();

            UIEquipment.I.CloseNotice();
        });
    }

    public void SetItemCount(int count)
    {
        itemCount = count;

        Text_itemCount.text = $"{count}";
    }

    public void SpawnItemBall()
    {
        Instantiate(spawnBall, Player.I.spawnBallPoint.position, Player.I.spawnBallPoint.rotation);
    }
}
