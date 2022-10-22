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

        StartCoroutine(_Talk(_text, popTime));
    }

    IEnumerator _Talk(string _text, float popTime)
    {
        // 局聪皋捞记 贸府

        yield return new WaitForSeconds(popTime);

        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
}
