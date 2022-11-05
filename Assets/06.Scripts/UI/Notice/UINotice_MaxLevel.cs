using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UINotice_MaxLevel : MonoBehaviour
{
    public static UINotice_MaxLevel I { get; private set; }

    Canvas canvas;

    SquashNStretch squashNStretch;

    public bool isShowing = false;

    protected void Awake()
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

    public IEnumerator ShowNoticeDelay(float delay, float timeout = -1f, UnityAction callback = null)
    {
        yield return new WaitForSeconds(delay);

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

    public void ShowNotice(float timeout = -1f, UnityAction callback = null)
    {
        isShowing = true;
        StartCoroutine(_ShowNotice(timeout, callback));
    }

    public IEnumerator _ShowNotice(float timeout, UnityAction callback)
    {
        canvas.enabled = true;

        squashNStretch.UI_Scaling_Show();

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
        if (isShowing == false) return;

        isShowing = false;

        squashNStretch.UI_Scaling_Hide(() =>
        {
            canvas.enabled = false;
        });
    }
}
