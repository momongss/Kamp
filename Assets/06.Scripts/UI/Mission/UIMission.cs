using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMission : MonoBehaviour
{
    Image progressBar;

    public int rewardExp;
    public int rewardMoney;

    public GameObject Obj_Exp;
    public GameObject Obj_Money;

    TextMeshProUGUI Text_Exp;
    TextMeshProUGUI Text_Money;

    float progress = 0;

    public MissionManager.Type missionType;

    private void Awake()
    {
        progressBar = GetComponent<Image>();

        Text_Exp = Obj_Exp.GetComponentInChildren<TextMeshProUGUI>();
        Text_Money = Obj_Money.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetExp();
        SetMoney();
    }

    public void AddProgress(float _progress)
    {
        progress += _progress;
        progressBar.fillAmount += progress;
    }

    public void SetExp()
    {
        int exp = MissionManager.I.GetExpReward(missionType);
        rewardExp = exp;
        Text_Exp.text = $"{exp}";

        if (exp == 0)
        {
            Obj_Exp.SetActive(false);
        }
    }

    public void SetMoney()
    {
        int money = MissionManager.I.GetMoneyReward(missionType);
        rewardMoney = money;
        Text_Money.text = $"{money}";

        if (money == 0)
        {
            Obj_Money.SetActive(false);
        }
    }
}
