using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class CookRespawnManager : MonoBehaviour
{
    public static CookRespawnManager I;

    public CookRespawn porkBellyRespawn;
    public CookRespawn stewRespawn;
    public CookRespawn marshmellowRespawn;
    public CookRespawn test01Respawn;
    public CookRespawn kochiRespawn;

    Dictionary<Food.Type, CookRespawn> cookDic;

    private void Awake()
    {
        I = this;

        cookDic = new Dictionary<Food.Type, CookRespawn>();

        cookDic.Add(Food.Type.PorkBelly, porkBellyRespawn);
        cookDic.Add(Food.Type.Stew, stewRespawn);
        cookDic.Add(Food.Type.Marshmellow, marshmellowRespawn);
        cookDic.Add(Food.Type.Test01, test01Respawn);
        cookDic.Add(Food.Type.KoChi, kochiRespawn);
    }

    public void Respawn(Food.Type foodType, int count)
    {
        CookRespawn cookRespawn = cookDic[foodType];

        cookRespawn.Respawn();
    }
}
