using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMap : MonoBehaviour
{
    public UISelectMap.MapType type;
    public Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnClick()
    {
        UISelectMap.I.SelectMap(type);
    }
}
