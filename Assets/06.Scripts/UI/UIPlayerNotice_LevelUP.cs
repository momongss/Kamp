using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIPlayerNotice_LevelUP : MonoBehaviour
{
    public static UIPlayerNotice_LevelUP I { get; private set; }

    public TextMeshProUGUI text;
    public GameObject Panel;

    SquashNStretch squashNStretch;

    private void Awake()
    {
        I = this;

        transform.GetChild(0).gameObject.SetActive(false);

        squashNStretch = Panel.GetComponent<SquashNStretch>();

        print(squashNStretch);
    }

    private void Start()
    {
        CloseNotice();
    }

    public void ShowNotice(int level, float timeout = -1f, UnityAction callback = null)
    {
        Panel.gameObject.SetActive(true);

        print("show");

        StartCoroutine(_ShowNotice(level, timeout, callback));
    }

    public IEnumerator _ShowNotice(int level, float timeout, UnityAction callback)
    {
        text.text = $"{level} ·¹º§";

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
            Panel.gameObject.SetActive(false);
        });
    }
}
