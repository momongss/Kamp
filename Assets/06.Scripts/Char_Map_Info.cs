using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Char_Map_Info : MonoBehaviour
{
    public static Char_Map_Info I;

    CharacterDataList characterDataList;

    public HashSet<Character.Type> selected_charaters = new HashSet<Character.Type>();

    const string character_dataPath = "/Character.json";

    private void Awake()
    {
        I = this;

        print(Application.persistentDataPath);

        LoadHavingCharacterData();
    }

    public List<CharacterData> GetHavingCharacters()
    {
        List<CharacterData> c_data = new List<CharacterData>();

        foreach (var c in characterDataList.characterDatas)
        {
            if (c.isHaving) c_data.Add(c);
        }

        return c_data;
    }

    void LoadHavingCharacterData()
    {
        if (JsonData.isFileExist(character_dataPath) == false)
        {
            characterDataList = new CharacterDataList();
            List<CharacterData> characterDatas = new List<CharacterData>();

            characterDatas.Add(new CharacterData(true, Character.Type.Penguin, true, 0));
            characterDatas.Add(new CharacterData(true, Character.Type.Sheep, false, 0));
            characterDatas.Add(new CharacterData(true, Character.Type.Cat, false, 0));
            characterDatas.Add(new CharacterData(true, Character.Type.Duck, false, 0));
            characterDatas.Add(new CharacterData(true, Character.Type.GGUM, false, 0));

            characterDataList.characterDatas = characterDatas;

            JsonData.SaveObj(characterDataList, character_dataPath);
        }
        else
        {
            string s_char = JsonData.LoadJson(character_dataPath);
            characterDataList = JsonUtility.FromJson<CharacterDataList>(s_char);
        }
    }

    public void AddHavingCharacter(Character.Type type)
    {
        CharacterData c_data = characterDataList.characterDatas.Find(a => a.type == type);

        if (c_data.isHaving)
        {
            Debug.LogWarning($"{type} 보유중인 캐릭터 추가 이벤트 발생. 로직에러 위험");
            return;
        }

        c_data.isHaving = true;
        JsonData.SaveObj(characterDataList, character_dataPath);
    }
}

[Serializable]
public class CharacterDataList
{
    public List<CharacterData> characterDatas;
}

[Serializable]
public class CharacterData
{
    public Character.Type type;
    public bool isHaving;
    public int friendliness;

    public CharacterData()
    {

    }

    public CharacterData(bool isInitialize, Character.Type _type, bool _isHaving, int _friendliness)
    {
        if (isInitialize)
        {
            type = _type;
            isHaving = _isHaving;
            friendliness = _friendliness;
        }
    }
}