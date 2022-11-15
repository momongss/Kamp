using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMissionTable : UINotice
{
    public static UIMissionTable I { get; private set; }

    public Transform tr_missionUI_Parent;

    public UIMission Prefab_MissionUI_Tent;
    public UIMission Prefab_MissionUI_Cook;
    public UIMission Prefab_MissionUI_Campfire;

    protected override void Awake()
    {
        I = this;

        base.Awake();
    }

    public void SetMissions(Mission[] missions)
    {
        foreach (var m in missions)
        {
            switch (m.type)
            {
                case MissionManager.Type.Tent:
                    m.uiMission = Instantiate(Prefab_MissionUI_Tent, tr_missionUI_Parent);
                    break;
                case MissionManager.Type.Cook:
                    m.uiMission = Instantiate(Prefab_MissionUI_Cook, tr_missionUI_Parent);
                    break;
                case MissionManager.Type.CampFire:
                    m.uiMission = Instantiate(Prefab_MissionUI_Campfire, tr_missionUI_Parent);
                    break;
            }
        }
    }
}
