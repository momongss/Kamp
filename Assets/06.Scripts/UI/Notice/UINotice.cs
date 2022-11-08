using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UINotice : MonoBehaviour
{
    public TextMeshProUGUI text;

    public GameObject Panel;

    SquashNStretch squashNStretch;

    protected bool isShowing = false;

    protected virtual void Awake()
    {
        squashNStretch = GetComponentInChildren<SquashNStretch>(true);

        if (Panel == null) Panel = transform.GetChild(0).gameObject;
        Panel.SetActive(false);
    }

    private void Start()
    {
        CloseNotice();
    }

    public virtual void ShowNotice(string content = null, float timeout = -1f, UnityAction callback = null)
    {
        isShowing = true;
        StartCoroutine(_ShowNotice(content, timeout, callback));
    }

    public virtual IEnumerator _ShowNotice(string content, float timeout, UnityAction callback)
    {
        if (content != null)
        {
            text.text = content;
        }

        Panel.SetActive(true);

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
            Panel.SetActive(false);
        });
    }
}
