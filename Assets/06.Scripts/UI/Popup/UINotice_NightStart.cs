using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UINotice_NightStart : MonoBehaviour
{
    public static UINotice_NightStart I { get; private set; }

    Canvas canvas;

    SquashNStretch squashNStretch;

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

    public void ShowNotice(float timeout = -1f, UnityAction callback = null)
    {
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
        squashNStretch.UI_Scaling_Hide(() =>
        {
            canvas.enabled = false;
        });
    }
}
