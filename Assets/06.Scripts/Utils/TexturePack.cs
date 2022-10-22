using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePack : MonoBehaviour
{
    public static TexturePack Instance;

    public Texture meatDemoCooked;

    [Header("Kochi Meat")]
    public Mesh Kochi_Meat_0;
    public Mesh Kochi_Meat_1;
    public Mesh Kochi_Meat_2;

    [Header("Kochi Meat Cooked")]
    public Mesh Kochi_Meat_Cooked_0;
    public Mesh Kochi_Meat_Cooked_1;
    public Mesh Kochi_Meat_Cooked_2;

    private void Awake()
    {
        Instance = this;
    }

    public Mesh GetKochiMesh(Mesh kochiMeatPiece)
    {
        // ��ġ ������ �̸��� "0", "1", "2" �����Ѵ�.\
        string meshName = Utils.RemoveLastWord(kochiMeatPiece.name);

        if (meshName == Kochi_Meat_0.name)
        {
            return Kochi_Meat_Cooked_0;
        }
        else if (meshName == Kochi_Meat_1.name)
        {
            return Kochi_Meat_Cooked_1;
        }
        else if (meshName == Kochi_Meat_2.name)
        {
            return Kochi_Meat_Cooked_2;
        }

        Debug.LogError($"�ش��ϴ� ��ġ��Ⱑ ����. {Kochi_Meat_1.GetInstanceID()} {Kochi_Meat_2.GetInstanceID()}");
        Debug.Log($"{Kochi_Meat_0} {Kochi_Meat_0.name} {Kochi_Meat_0.GetInstanceID()}");

        return null;
    }
}
