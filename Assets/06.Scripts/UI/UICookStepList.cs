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
    int currStepCount = 0;

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
        currStepCount = 0;

        ShowStep(currStepCount);
    }

    void ShowStep(int step)
    {
        foreach (var g in stepList)
        {
            g.gameObject.SetActive(false);
        }

        stepList[step].gameObject.SetActive(true);
    }

    public void OnCompleteWork(Food food, Food.Action action)
    {
        UICookStep step = stepList[currStepCount];


        if (!(step.type == food.foodType && step.action == action)) return;

        step.workCount++;

        if (step.workCount >= step.totalWorkCount)
        {
            OnCompleteStep(food);
        }
    }

    // ���� Step �� Work ���ֿ̹� ���� ������. �����ؾ���.
    public void OnCompleteStep(Food food)
    {
        // print($"{currStep}, {stepCount}");
        if (currStepCount >= stepCount)
        {
            Debug.LogWarning("step ���� �߻�. step ���� Ȯ���ʿ�");
            return;
        }
        else if (currStepCount == stepCount - 1)
        {
            OnCompleteAllStep(food);
        }
        else
        {
            currStepCount++;
            ShowStep(currStepCount);
        }
    }

    void OnCompleteAllStep(Food food)
    {
        UICookStepManager.I.OnCompleteAllStep(food);
    }
}
