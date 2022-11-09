using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager
{
    public static Dictionary<int, Character.Type> characterUnlockLevelBook = new Dictionary<int, Character.Type>() {
        { 0, Character.Type.Penguin },
        { 1, Character.Type.Sheep },
        { 2, Character.Type.Duck },
        { 3, Character.Type.Cat },
        { 4, Character.Type.GGUM },
    };

    public static Character.Type GetCharacter(int level)
    {
        if (characterUnlockLevelBook.ContainsKey(level))
        {
            return characterUnlockLevelBook[level];
        }
        else
        {
            return Character.Type.None;
        }
    }

    public static List<Character.Type> GetHavingCharacters(int level)
    {
        List<Character.Type> list = new List<Character.Type>();

        foreach (int lv in characterUnlockLevelBook.Keys)
        {
            if (lv <= level)
            {
                list.Add(characterUnlockLevelBook[lv]);
            }
        }

        return list;
    }
}
