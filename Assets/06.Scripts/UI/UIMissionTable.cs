using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMissionTable : UINotice
{
    public static UIMissionTable I { get; private set; }

    public Transform tr_missionUI_Parent;

    public GameObject Prefab_MissionUI_Tent;
    public GameObject Prefab_MissionUI_Cook;

    protected override void Awake()
    {
        I = this;

        base.Awake();
    }

    public void SetMissions(MissionManager.Type[] missions)
    {
        foreach (var t in missions)
        {
            print(t);
            switch (t)
            {
                case MissionManager.Type.Tent:
                    Instantiate(Prefab_MissionUI_Tent, tr_missionUI_Parent);
                    break;
                case MissionManager.Type.Cook:
                    Instantiate(Prefab_MissionUI_Cook, tr_missionUI_Parent);
                    break;
            }
        }
    }
}
