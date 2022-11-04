using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance { get; private set; }

    int level = 0;
    int exp = 0;

    UnityEvent<int> level_event;
    UnityEvent<int> exp_event;

    const string exp_dataPath = "/Exp.json";

    int[] required_exp = new int[] { 30, 50, 80, 120, 150, 180 };

    private void Awake()
    {
        print(Application.persistentDataPath);

        Instance = this;

        if (JsonData.isFileExist(exp_dataPath) == false)
        {
            JsonData.SaveJson($"{exp}", exp_dataPath);
        }

        exp = int.Parse(JsonData.LoadJson(exp_dataPath));
        level = Calculate_Level();

        exp_event = new UnityEvent<int>();
        level_event = new UnityEvent<int>();
    }

    public void Subscribe_Exp(UnityAction<int> e)
    {
        if (exp_event == null) exp_event = new UnityEvent<int>();

        exp_event.AddListener(e);
        e(exp);
    }

    public void Subscribe_Level(UnityAction<int> e)
    {
        if (level_event == null) level_event = new UnityEvent<int>();

        level_event.AddListener(e);
        e(level);
    }

    public void Add_Exp(int rewardExp)
    {
        print($"{rewardExp} exp");

        Set_Exp(exp + rewardExp);
        bool isLevelUP = Set_Level();
        if (!isLevelUP)
        {
            UIPlayerNotice.I.ShowNotice($"+{rewardExp}exp!!", 4f);
        }
    }

    public void Add_Exp(int rewardExp, string msg)
    {
        print($"{rewardExp} exp");

        Set_Exp(exp + rewardExp);
        bool isLevelUP = Set_Level();
        if (!isLevelUP)
        {
            UIPlayerNotice.I.ShowNotice(msg, 4f);
        }
    }

    int Calculate_Level()
    {
        float level_exp = 0;

        for (int lv = 0; lv < required_exp.Length; lv++)
        {
            level_exp += required_exp[lv];

            if (exp < level_exp)
            {
                return lv;
            }
        }

        return required_exp.Length - 1;
    }

    void Set_Exp(int _exp)
    {
        exp = _exp;
        exp_event.Invoke(exp);

        JsonData.SaveJson($"{exp}", exp_dataPath);
    }

    bool Set_Level()
    {
        int _level = Calculate_Level();

        if (level == _level) return false;

        level = _level;
        level_event.Invoke(level);

        print("Sel level");
        UIPlayerNotice_LevelUP.I.ShowNotice(level);

        return true;
    }

    public int Get_Level()
    {
        return level;
    }

    public float Get_Exp()
    {
        return exp;
    }
}
