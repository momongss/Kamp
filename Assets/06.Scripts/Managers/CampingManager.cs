using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class CampingManager : MonoBehaviour
{
    public enum State { Greet_Characters }
    public State state;

    public Character[] characterList;
    public List<Character> activeCharacterList;

    public string[] speach_Arrival_To_Camp = new string[] { "와!! 신난다!", "아함~", "안녕~!!" };

    public static CampingManager Instance { get; private set; }

    UnityAction[] campingStory;
    int storyIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        campingStory = new UnityAction[5];
        campingStory[0] = () =>
        {
            print("X");
            UI_Notice_X_tutorial.I.ShowNotice(null, -1, PlayNextStep);
        };

        campingStory[1] = () =>
        {
            print("Y");
            UI_Notice_Y_tutorial.I.ShowNotice(null, 7f, PlayNextStep);
        };

        campingStory[2] = () =>
        {
            SpawnCharacters();
            UIPlayerNotice.I.ShowNotice("캐릭터들이 도착했어요!!", 4f, PlayNextStep);
        };

        campingStory[3] = () =>
        {
            UIMissionTable.I.ShowNotice(null, 4f, PlayNextStep);
        };

        campingStory[4] = () =>
        {
            // 식사시간
            UIPlayerNotice.I.ShowNotice("식사 시간이예요! 요리를 하세요", 4f);
            CookManager.I.StartCooking();
        };

        PlayNextStep();
    }

    void PlayNextStep()
    {
        print($"Play {storyIndex}");
        campingStory[storyIndex]();
        storyIndex++;
    }

    public void SpawnCharacters()
    {
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
}
