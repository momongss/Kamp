using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICookStepManager : UIPanel
{
    public static UICookStepManager I;

    Dictionary<Food.Type, UICookStepList> stepListDic = new Dictionary<Food.Type, UICookStepList>();

    List<UICookStepList> StepUIList = new List<UICookStepList>();

    UICookStepList currStep;

    private void Awake()
    {
        I = this;

        foreach (var t in Utils.GetChildren(transform))
        {
            UICookStepList stepList = t.GetComponent<UICookStepList>();
            stepListDic.Add(stepList.cookType, stepList);
        }
    }

    public void StartCookStep(Food.Type foodType, int count)
    {
        Show();
        HideAllUI();

        currStep = stepListDic[foodType];
        currStep.StartStep(count);
    }

    public void OnCompleteWork(Food.Type type, Food.Action action)
    {
        if (currStep == null)
        {
            return;
        }

        currStep.OnCompleteWork(type, action);
    }

    public void OnCompleteAllStep()
    {
        CookManager.Instance.OnCompleteCooking();
    }

    void HideAllUI()
    {
        foreach (UICookStepList g in StepUIList)
        {
            g.Hide();
        }
    }
}
