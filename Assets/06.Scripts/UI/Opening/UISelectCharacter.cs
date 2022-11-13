using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISelectCharacter : UIPanel
{
    public static UISelectCharacter I { get; private set; }

    public Transform T_CharacterSelectButtons;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        ToggleCharacter[] c_buttons = T_CharacterSelectButtons.GetComponentsInChildren<ToggleCharacter>();

        foreach (var c in c_buttons)
        {
            c.gameObject.SetActive(false);
        }

        int level = ExpManager.Instance.Get_Level();

        foreach (Character.Type type in StatManager.GetHavingCharacters(level))
        {
            foreach (var c in c_buttons)
            {
                if (c.type == type)
                {
                    c.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }
}
