using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCommunicator : MonoBehaviour
{
    public GameObject TalkCanvas;
    public TMP_Text narrationText;

    private void Start()
    {
        TalkCanvas.SetActive(false);

        ShowNarration("¾È³ç~~~~");
    }

    public void ShowNarration(string text, float time = 4f)
    {
        StartCoroutine(NarrationNormal(text, time));
    }

    public void Ask(string question)
    {
        narrationText.text = question;
        TalkCanvas.SetActive(true);
    }

    public void Answer()
    {
        TalkCanvas.SetActive(false);
    }

    IEnumerator NarrationNormal(string text, float time)
    {
        narrationText.text = text;
        TalkCanvas.SetActive(true);

        yield return new WaitForSeconds(time);
        TalkCanvas.SetActive(false);
    }
}
