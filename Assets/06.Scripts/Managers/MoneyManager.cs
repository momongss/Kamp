using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager I { get; private set; }

    public int money = 100;

    const string money_dataPath = "/Money.json";

    UnityEvent<int> Event_money = new UnityEvent<int>();

    public TextMeshProUGUI[] textList;

    private void Awake()
    {
        I = this;

        if (JsonData.isFileExist(money_dataPath) == false)
        {
            SaveMoney(money);
        }
        else
        {
            money = int.Parse(JsonData.LoadJson(money_dataPath));
        }

        Event_money.Invoke(money);

        foreach (TextMeshProUGUI text in textList)
        {
            text.text = $"{money}";
        }
    }

    public void SubscribeMoney(UnityAction<int> e)
    {
        Event_money.AddListener(e);
    }

    public void AddMoney(int quantity)
    {
        SaveMoney(money + quantity);
    }

    public bool UseMoney(int quantity)
    {
        if (money >= quantity)
        {
            SaveMoney(money - quantity);
            return true;
        }

        return false;
    }

    void SaveMoney(int _money)
    {
        money = _money;

        Event_money.Invoke(money);

        foreach (TextMeshProUGUI text in textList)
        {
            text.text = $"{money}";
        }

        JsonData.SaveJson($"{money}", money_dataPath);
    }
}
