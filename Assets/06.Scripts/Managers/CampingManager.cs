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
    public List<Character> activeCharacterList;

    public string[] speach_Arrival_To_Camp = new string[] { "와!! 신난다!", "아함~", "안녕~!!" };

    public static CampingManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SetState(State.Greet_Characters, 2f));
    }

    public void SpawnCharacters()
    {
        print(Char_Map_Info.I);

        if (Char_Map_Info.I != null)
        {
            HashSet<Character.Type> selected_characters = Char_Map_Info.I.selected_charaters;

            foreach (Character c in characterList)
            {
                if (selected_characters.Contains(c.type))
                {
                    c.gameObject.SetActive(true);

                    activeCharacterList.Add(c);
                }
            }
        }
        else
        {
            foreach (Character c in characterList)
            {
                c.gameObject.SetActive(true);
                activeCharacterList.Add(c);
            }
        }

    }

    public void OnCompleteMissionRoutines()
    {

    }

    IEnumerator SetState(State newState, float delay)
    {
        yield return new WaitForSeconds(delay);

        state = newState;

        switch (state)
        {
            case State.Greet_Characters:
                UIPlayerNotice.I.ShowNotice("캐릭터들이 도착했어요!! 마중 나가볼까요??");
                break;
        }
    }
}
