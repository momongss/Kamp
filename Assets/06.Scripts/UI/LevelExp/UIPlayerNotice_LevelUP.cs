using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIPlayerNotice_LevelUP : MonoBehaviour
{
    public static UIPlayerNotice_LevelUP I { get; private set; }

    public TextMeshProUGUI text;
    public GameObject Panel;

    SquashNStretch squashNStretch;

    public Transform T_characterParent;

    CharacterIndicator[] characterUIList;

    bool isShowing = false;

    private void Awake()
    {
        I = this;

        transform.GetChild(0).gameObject.SetActive(false);

        squashNStretch = Panel.GetComponent<SquashNStretch>();

        characterUIList = T_characterParent.GetComponentsInChildren<CharacterIndicator>(true);
    }

    private void Start()
    {
        CloseNotice();
    }

    public void ShowNotice(int level, float timeout = -1f, UnityAction callback = null)
    {
        isShowing = true;

        Panel.gameObject.SetActive(true);

        StartCoroutine(_ShowNotice(level, timeout, callback));
    }

    public IEnumerator _ShowNotice(int level, float timeout, UnityAction callback)
    {
        Character.Type type = StatManager.characterUnlockLevelBook[level];

        if (type != Character.Type.None)
        {
            foreach (var c in characterUIList)
            {
                if (c.type == type)
                {
                    c.gameObject.SetActive(true);
                } else
                {
                    c.gameObject.SetActive(false);
                }
            }
        }

        text.text = $"{level} ·¹º§";

        squashNStretch.UI_Scaling_Show();

        if (timeout != -1)
        {
            yield return new WaitForSeconds(timeout);

            CloseNotice();
        }

        if (callback != null)
        {
            callback();
        }
    }

    public void CloseNotice()
    {
        print(isShowing);
        if (isShowing == false) return;

        isShowing = false;

        squashNStretch.UI_Scaling_Hide(() =>
        {
            Panel.gameObject.SetActive(false);
        });
    }
}
