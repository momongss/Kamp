using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextLevel : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        ExpManager.Instance.Subscribe_Level((int level) =>
        {
            text.text = $"{level}";
        });
    }
}
