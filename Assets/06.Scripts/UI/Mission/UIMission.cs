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

    public void AddProgress(float _progress)
    {
        progress += _progress;
        progressBar.fillAmount += progress;
    }

    public void SetExp(int exp)
    {
        rewardExp = exp;
        Text_Exp.text = $"{exp}";
    }

    public void SetMoney(int money)
    {
        rewardMoney = money;
        Text_Money.text = $"{money}";
    }
}
