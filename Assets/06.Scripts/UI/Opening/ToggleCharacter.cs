using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCharacter : MonoBehaviour
{
    public Character.Type type;

    private void Awake()
    {
        Toggle toggle = GetComponent<Toggle>();
    }

    public void OnChange(bool isOn)
    {
        if (isOn)
        {
            SelectSceneManager.I.AddCharacter(type);
        }
        else
        {
            SelectSceneManager.I.RemoveCharacter(type);
        }
    }
}
