using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Map_Info : MonoBehaviour
{
    public static Char_Map_Info I;

    public HashSet<Character.Type> selected_charaters = new HashSet<Character.Type>();
    public UISelectMap.MapType selected_mapType;

    private void Awake()
    {
        I = this;
    }
}
