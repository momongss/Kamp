using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanel : MonoBehaviour
{
    public QuestionButton questionButtonPrefab;

    public void SetQuestions()
    {
        // 질문 목록 초기화
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }


    }
}
