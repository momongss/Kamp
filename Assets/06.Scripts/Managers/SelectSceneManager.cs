using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneManager : MonoBehaviour
{
    public static SelectSceneManager I { get; private set; }

    UISelectMap.MapType mapType;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        UISelectCharacter.I.Show();
        UISelectMap.I.Hide();
    }

    public void StartCamping()
    {
        DontDestroyOnLoad(Char_Map_Info.I.gameObject);

        SceneManager.LoadScene(Scene.FoodDemo);
    }

    public void SelectMap(UISelectMap.MapType _mapType)
    {
        mapType = _mapType;
    }

    public void AddCharacter(Character.Type type)
    {
        if (Char_Map_Info.I.selected_charaters.Contains(type) == false)
        {
            Char_Map_Info.I.selected_charaters.Add(type);
        }
    }

    public void RemoveCharacter(Character.Type type)
    {
        if (Char_Map_Info.I.selected_charaters.Contains(type) == true)
        {
            Char_Map_Info.I.selected_charaters.Remove(type);
        }
    }

    public void SelectCharacters()
    {
        UISelectCharacter.I.Hide();
        UISelectMap.I.Show();
    }
}
