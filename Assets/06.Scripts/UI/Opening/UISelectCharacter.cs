using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
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

        foreach (CharacterData data in Char_Map_Info.I.GetHavingCharacters())
        {
            foreach (var c in c_buttons)
            {
                if (c.type == data.type)
                {
                    c.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }
}
