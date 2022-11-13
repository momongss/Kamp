using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIPlayerNotice_LevelUP : UINotice
{
    public static UIPlayerNotice_LevelUP I { get; private set; }

    public Transform T_characterParent;

    CharacterIndicator[] characterUIList;

    protected override void Awake()
    {
        I = this;
        characterUIList = T_characterParent.GetComponentsInChildren<CharacterIndicator>(true);

        base.Awake();
    }

    private void Start()
    {
        CloseNotice();
    }

    public void ShowNotice(int level, float timeout = -1f, UnityAction callback = null)
    {
        isShowing = true;

        Panel.gameObject.SetActive(true);

        Character.Type type = StatManager.characterUnlockLevelBook[level];

        noticeCloseCallback = callback;

        if (type != Character.Type.None)
        {
            foreach (var c in characterUIList)
            {
                if (c.type == type)
                {
                    c.gameObject.SetActive(true);
                }
                else
                {
                    c.gameObject.SetActive(false);
                }
            }
        }

        StartCoroutine(_ShowNotice($"{level} ·¹º§", timeout));
    }
}
