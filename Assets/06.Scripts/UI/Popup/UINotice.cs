using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class UINotice : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Text textLegacy;

    public GameObject Panel;

    SquashNStretch squashNStretch;

    protected bool isShowing = false;

    protected UnityAction noticeCloseCallback = null;

    Button Button_close;

    protected virtual void Awake()
    {
        squashNStretch = GetComponentInChildren<SquashNStretch>(true);

        if (Panel == null) Panel = transform.GetChild(0).gameObject;
        Panel.SetActive(false);

        Button_close = Panel.GetComponent<Button>();
        if (Button_close)
        {
            Button_close.onClick.AddListener(() =>
            {
                CloseNotice();
            });
        }

        CloseNotice();
    }

    public virtual void ToggleNotice()
    {
        if (isShowing)
        {
            CloseNotice();
        }
        else
        {
            ShowNotice();
        }
    }

    public virtual void ShowNotice(string content = null, float timeout = -1f, UnityAction callback = null)
    {
        isShowing = true;

        if (callback != null) noticeCloseCallback = callback;
        StartCoroutine(_ShowNotice(content, timeout));
    }

    public virtual IEnumerator _ShowNotice(string content, float timeout)
    {
        if (content != null)
        {
            if (textLegacy)
            {
                textLegacy.text = content;
            }
            else if (text)
            {
                text.text = content;
            }
        }

        Panel.SetActive(true);

        squashNStretch.UI_Scaling_Show();

        print(timeout);
        if (timeout != -1)
        {
            yield return new WaitForSeconds(timeout);
            print("Close");

            CloseNotice();
        }
    }

    public void CloseNotice()
    {
        if (isShowing == false) return;

        isShowing = false;

        squashNStretch.UI_Scaling_Hide(() =>
        {
            Panel.SetActive(false);

            if (noticeCloseCallback != null)
            {
                print("Close");
                noticeCloseCallback();
                noticeCloseCallback = null;
            }
        });
    }
}
