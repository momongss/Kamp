using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextExp : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        ExpManager.Instance.Subscribe_Exp((float exp) =>
        {
            text.text = $"{exp}";
        });
    }
}
