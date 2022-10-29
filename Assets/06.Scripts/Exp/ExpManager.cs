using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance { get; private set; }

    int level = 0;
    float exp = 0;

    UnityEvent<int> level_event;
    UnityEvent<float> exp_event;

    const string exp_dataPath = "/Exp.json";

    float[] required_exp = new float[] { 30, 50, 80, 120, 150, 180 };

    private void Awake()
    {
        Instance = this;

        if (JsonData.isFileExist(exp_dataPath) == false)
        {
            JsonData.SaveJson($"{exp}", exp_dataPath);
        }

        exp = float.Parse(JsonData.LoadJson(exp_dataPath));
        level = Calculate_Level();
    }

    public void Subscribe_Exp(UnityAction<float> e)
    {
        if (exp_event == null) exp_event = new UnityEvent<float>();

        exp_event.AddListener(e);
        e(exp);
    }

    public void Subscribe_Level(UnityAction<int> e)
    {
        if (level_event == null) level_event = new UnityEvent<int>();

        level_event.AddListener(e);
        e(level);
    }

    public void Add_Exp(float _exp_delta)
    {
        Set_Exp(exp + _exp_delta);
        Set_Level();
    }

    int Calculate_Level()
    {
        float level_exp = 0;

        for (int lv = 0; lv < required_exp.Length; lv++) {
            level_exp += required_exp[lv];

            if (exp < level_exp)
            {
                return lv;
            }
        }

        return required_exp.Length - 1;
    }

    void Set_Exp(float _exp)
    {
        exp = _exp;
        exp_event.Invoke(exp);

        JsonData.SaveJson($"{exp}", exp_dataPath);
    }

    void Set_Level()
    {
        int _level = Calculate_Level();

        if (level == _level) return;

        level = _level;
        level_event.Invoke(level);
    }

    public int Get_Level()
    {
        return level;
    }

    public float Get_Exp()
    {
        return exp;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
