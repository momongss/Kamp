using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectMap : UIPanel
{
    public enum MapType
    {
        Forrest,
        Ice
    }

    public static UISelectMap I { get; private set; }

    public ToggleMap[] toggleList;

    public Color originColor;
    public Color selectedColor;


    private void Awake()
    {
        I = this;

        toggleList = GetComponentsInChildren<ToggleMap>();

        if (toggleList.Length == 0)
        {
            Debug.LogError("¸Ê ¹öÆ° ¾øÀ½.");
            return;
        }

        originColor = toggleList[0].gameObject.GetComponent<Image>().color;
    }

    private void Start()
    {
        SelectMap(MapType.Forrest);
    }

    public void SelectMap(MapType mapType)
    {
        foreach (ToggleMap t in toggleList)
        {
            if (t.type == mapType)
            {
                t.gameObject.GetComponent<Image>().color = selectedColor;
                SelectSceneManager.I.SelectMap(t.type);
            }
            else
            {
                t.gameObject.GetComponent<Image>().color = originColor;
            }
        }
    }
}
