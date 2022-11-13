using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour
{
    public Color originColor;
    public Color selectedColor;

    bool isSelected = false;

    Image image;

    public UnityEvent<bool> OnToggle;

    private void Awake()
    {
        image = GetComponent<Image>();
        originColor = image.color;
    }

    public void Toggle()
    {
        isSelected = !isSelected;

        print(isSelected);

        if (isSelected == false)
        {
            Deselect();
        }
        else
        {
            Select();
        }

        OnToggle.Invoke(isSelected);
    }

    void Select()
    {
        image.color = selectedColor;
    }

    void Deselect()
    {
        image.color = originColor;
    }
}
