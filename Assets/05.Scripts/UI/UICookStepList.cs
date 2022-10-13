using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICookStepList : UIPanel
{
    public Food.Type cookType;

    Transform stepListParent;   // 첫번째 child

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
            if (Food.canPerformedMultiTimes.Contains(step.action))
            {
                step.SetTotalStepCount(count);
            }
            else
            {
                step.SetTotalStepCount(1);
            }


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

        if (!(step.type == type && step.action == action)) return;

        step.stepCount++;
        if (step.stepCount >= step.totalStepCount)
        {
            OnCompleteStep();
        }
    }

    // 지금 Step 과 Work 네이밍에 오류 존재함. 주의해야함.
    public void OnCompleteStep()
    {
        print($"{currStep}, {stepCount}");
        if (currStep >= stepCount)
        {
            Debug.LogWarning("step 오류 발생. step 로직 확인필요");
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
