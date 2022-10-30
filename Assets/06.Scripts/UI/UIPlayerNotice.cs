using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerNotice : MonoBehaviour
{
    public static UIPlayerNotice Instance { get; private set; }

    public TextMeshProUGUI text;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CloseNotice();
    }

    public void ShowNotice(string content, float timeout = -1f)
    {
        text.text = content;
        gameObject.SetActive(true);

        if (timeout != -1)
        {
            Invoke("CloseNotice", timeout);
        }
    }

    public void CloseNotice()
    {
        gameObject.SetActive(false);
    }
}
