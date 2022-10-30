using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    public List<Character> characterList = new List<Character>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterCharacter(Character c)
    {
        characterList.Add(c);
    }

    public void SetFollowPlayer()
    {
        SetCharacters(Character.State.FollowPlayer);
    }

    public void SetFreeCharacters()
    {
        SetCharacters(Character.State.WalkAround);
    }

    void SetCharacters(Character.State state)
    {
        foreach (Character c in characterList)
        {
            c.ChangeState(state);
        }
    }
}
