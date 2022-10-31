using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

public class UIPlayerNotice : MonoBehaviour
{
    public static UIPlayerNotice I { get; private set; }

    public TextMeshProUGUI text;

    Canvas canvas;

    SquashNStretch squashNStretch;

    private void Awake()
    {
        I = this;

        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        squashNStretch = GetComponentInChildren<SquashNStretch>();
    }

    private void Start()
    {
        CloseNotice();
    }

    public IEnumerator ShowNoticeDelay(string content, float delay, float timeout = -1f, UnityAction callback = null)
    {
        yield return new WaitForSeconds(delay);

        text.text = content;
        canvas.enabled = true;

        if (timeout != -1)
        {
            yield return new WaitForSeconds(timeout);

            CloseNotice();
        }

        if (callback != null)
        {
            callback();
        }
    }

    public void ShowNotice(string content, float timeout = -1f, UnityAction callback = null)
    {
        StartCoroutine(_ShowNotice(content, timeout, callback));
    }

    public IEnumerator _ShowNotice(string content, float timeout, UnityAction callback)
    {
        text.text = content;
        canvas.enabled = true;

        squashNStretch.UI_Scaling();

        if (timeout != -1)
        {
            yield return new WaitForSeconds(timeout);

            CloseNotice();
        }

        if (callback != null)
        {
            callback();
        }
    }

    public void CloseNotice()
    {
        canvas.enabled = false;
    }
}
