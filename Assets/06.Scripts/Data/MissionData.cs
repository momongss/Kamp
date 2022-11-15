using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionData : MonoBehaviour
{
    bool isInitialized = false;

    public Mission[][] datas;

    public int missionDaycount = 10;

    public Mission[] GetMissionsOfDay(int day)
    {
        Init();

        if (day < 0 || day >= missionDaycount)
        {
            Mission[] missions = new Mission[2];
            missions[0] = new Mission(MissionManager.Type.Cook);
            missions[1] = new Mission(MissionManager.Type.Tent);

            return missions;
        }
        else
        {
            return datas[day];
        }
    }

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        if (isInitialized) return;
        isInitialized = true;

        datas = new Mission[missionDaycount][];

        datas[0] = new Mission[3];

        datas[0][0] = new Mission(MissionManager.Type.Cook, 4);
        datas[0][1] = new Mission(MissionManager.Type.Tent);
        datas[0][2] = new Mission(MissionManager.Type.CampFire);

        datas[1] = new Mission[2];

        datas[1][0] = new Mission(MissionManager.Type.Cook);
        datas[1][1] = new Mission(MissionManager.Type.Tent);

        datas[2] = new Mission[2];

        datas[2][0] = new Mission(MissionManager.Type.Cook);
        datas[2][1] = new Mission(MissionManager.Type.Tent);

        datas[3] = new Mission[2];

        datas[3][0] = new Mission(MissionManager.Type.Cook);
        datas[3][1] = new Mission(MissionManager.Type.Tent);

        datas[4] = new Mission[2];

        datas[4][0] = new Mission(MissionManager.Type.Cook);
        datas[4][1] = new Mission(MissionManager.Type.Tent);

        datas[5] = new Mission[2];

        datas[5][0] = new Mission(MissionManager.Type.Cook);
        datas[5][1] = new Mission(MissionManager.Type.Tent);

        datas[6] = new Mission[2];

        datas[6][0] = new Mission(MissionManager.Type.Cook);
        datas[6][1] = new Mission(MissionManager.Type.Tent);

        datas[7] = new Mission[2];

        datas[7][0] = new Mission(MissionManager.Type.Cook);
        datas[7][1] = new Mission(MissionManager.Type.Tent);

        datas[8] = new Mission[2];

        datas[8][0] = new Mission(MissionManager.Type.Cook);
        datas[8][1] = new Mission(MissionManager.Type.Tent);

        datas[9] = new Mission[2];

        datas[9][0] = new Mission(MissionManager.Type.Cook);
        datas[9][1] = new Mission(MissionManager.Type.Tent);
    }
}
