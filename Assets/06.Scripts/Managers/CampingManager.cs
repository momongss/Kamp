using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Analytics;

public class CampingManager : MonoBehaviour
{
    public enum State { Greet_Characters }
    public State state;

    public Character[] characterList;

    public string[] speach_Arrival_To_Camp = new string[] { "��!! �ų���!", "����~", "�ȳ�~!!" };

    public static CampingManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SetState(State.Greet_Characters, 5f));
    }

    public void SpawnCharacters()
    {
        foreach (Character c in characterList) {
            c.gameObject.SetActive(true);
        }
    }

    IEnumerator SetState(State newState, float delay)
    {
        yield return new WaitForSeconds(delay);

        state = newState;

        switch (state)
        {
            case State.Greet_Characters:
                UIPlayerNotice.Instance.ShowNotice("ĳ���͵��� �����߾��!! ���� ���������??");
                break;
        }
    }
}
