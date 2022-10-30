using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Talk(string _text)
    {
        text.text = _text;
        gameObject.SetActive(true);
    }

    public void Talk(string _text, float popTime)
    {
        text.text = _text;
        gameObject.SetActive(true);

        print(_text);

        StartCoroutine(_Hide(popTime));
    }

    IEnumerator _Hide(float popTime)
    {
        // 局聪皋捞记 贸府

        yield return new WaitForSeconds(popTime);

        gameObject.SetActive(false);
    }
}
