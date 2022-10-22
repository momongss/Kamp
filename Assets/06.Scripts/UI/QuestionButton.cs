using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestionButton : MonoBehaviour
{
    public TMP_Text questionText;
    public int questionID;

    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetQuestion(int qId, string question, UnityAction callback)
    {
        questionID = qId;
        questionText.text = question;
        button.onClick.AddListener(callback);
    }
}
