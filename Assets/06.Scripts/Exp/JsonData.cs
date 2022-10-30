using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonData
{
    public static void SaveObj(object obj, string path)
    {
        string text = JsonUtility.ToJson(obj);
        File.WriteAllText(Application.persistentDataPath + path, text);
    }

    public static void SaveJson(string json, string path)
    {
        File.WriteAllText(Application.persistentDataPath + path, json);
    }

    public static string LoadJson(string path)
    {
        return File.ReadAllText(Application.persistentDataPath + path);
    }

    public static bool isFileExist(string dataPath)
    {
        return File.Exists(Application.persistentDataPath + dataPath);
    }
}
