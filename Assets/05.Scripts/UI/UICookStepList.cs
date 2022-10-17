using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class UICookStepList : UIPanel
{
    public Food.Type cookType;

    Transform stepListParent;   // ù��° child

    List<UICookStep> stepList;
    int stepCount = 0;
    int currStep = 0;

    public void StartStep(int count)
    {
        Show();

        stepList = new List<UICookStep>();

        stepListParent = transform.GetChild(0);

        for (int i = 0; i < stepListParent.childCount; i++)
        {
            UICookStep step = stepListParent.GetChild(i).GetComponent<UICookStep>();
            step.StartStep();


            stepList.Add(step);
        }

        stepCount = stepList.Count;
        currStep = 0;

        ShowStep(currStep);
    }

    void ShowStep(int step)
    {
        foreach (var g in stepList)
        {
            g.gameObject.SetActive(false);
        }

        stepList[step].gameObject.SetActive(true);
    }

    public void OnCompleteWork(Food.Type type, Food.Action action)
    {
        UICookStep step = stepList[currStep];

        print($"{step.type}:{type}, {step.action}:{action}");

        if (!(step.type == type && step.action == action)) return;

        step.workCount++;
        if (step.workCount >= step.totalWorkCount)
        {
            OnCompleteStep();
        }

        print(step.workCount);
    }

    // ���� Step �� Work ���ֿ̹� ���� ������. �����ؾ���.
    public void OnCompleteStep()
    {
        print($"{currStep}, {stepCount}");
        if (currStep >= stepCount)
        {
            Debug.LogWarning("step ���� �߻�. step ���� Ȯ���ʿ�");
            return;
        }
        else if (currStep == stepCount - 1)
        {
            OnCompleteAllStep();
        }
        else
        {
            currStep++;
            ShowStep(currStep);
        }
    }

    void OnCompleteAllStep()
    {
        UICookStepManager.I.OnCompleteAllStep();
    }
}
