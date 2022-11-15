using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager I { get; private set; }

    public enum Type { Tent, Cook, CampFire, None }

    public List<Dictionary<string, object>> rewardMap_Exp;
    public List<Dictionary<string, object>> rewardMap_Money;

    public Transform Tr_missionUI_Parent;
    UIMission[] missionUIs;

    MissionData missionData;

    int max_day;

    public Mission[] missions;

    public int daycount = 16;

    public int currDay = 0;

    public UIMissionTable UI_MissionTable;

    private void Awake()
    {
        I = this;

        missionData = GetComponent<MissionData>();

        missions = missionData.GetMissionsOfDay(currDay);

        missionUIs = new UIMission[Tr_missionUI_Parent.childCount];

        Type[] missionTypes = GetMissionTypes(missions);

        for (int i = 0; i < Tr_missionUI_Parent.childCount; i++)
        {
            missionUIs[i] = Tr_missionUI_Parent.GetChild(i).GetComponent<UIMission>();

            if (missionTypes.Contains(missionUIs[i].missionType))
            {
                missionUIs[i].gameObject.SetActive(true);
            }
            else
            {
                missionUIs[i].gameObject.SetActive(false);
            }
        }

        rewardMap_Exp = CSVReader.Read("RewardMap_Exp");
        rewardMap_Money = CSVReader.Read("RewardMap_Money");

        max_day = rewardMap_Exp.Count - 1;

        UI_MissionTable.SetMissions(missions);
    }

    public Type[] GetMissionTypes(Mission[] _missions)
    {
        Type[] types = new Type[_missions.Length];

        for (int i = 0; i < _missions.Length; i++)
        {
            types[i] = missions[i].type;
        }

        return types;
    }

    public Mission GetMission(Type type)
    {
        foreach (Mission m in missions)
        {
            if (m.type == type) return m;
        }

        return null;
    }

    public void OnMissionComplete(Type _type)
    {
        Mission m = GetMission(_type);
        if (m == null) return;

        bool isMissionComplete = m.Progress();

        if (isMissionComplete)
        {
            GiveReward(_type);
            CampingManager.Instance.OnCompleteMissionRoutines();
        }
    }

    public int GetExpReward(Type _type)
    {

        if (currDay > max_day)
        {
            Debug.LogError($"만렙 이상의 레벨 발생. 만렙 : {max_day}, 발생한 레벨 : {currDay}");
        }

        int rewardExp;
        if (currDay < rewardMap_Exp.Count)
        {
            rewardExp = (int)rewardMap_Exp[currDay][_type.ToString()];
        }
        else
        {
            rewardExp = currDay * 100;
        }

        return rewardExp;
    }

    public int GetMoneyReward(Type _type)
    {
        int rewardMoney;
        if (currDay < rewardMap_Money.Count)
        {
            rewardMoney = (int)rewardMap_Money[currDay][_type.ToString()];
        }
        else
        {
            rewardMoney = currDay * 100;
        }

        return rewardMoney;
    }

    void GiveReward(Type _type, bool isCompleteAllMission = false)
    {
        int level = ExpManager.Instance.Get_Level();

        int rewardExp = GetExpReward(_type);
        int rewardMoney = GetMoneyReward(_type);

        MoneyManager.I.AddMoney(rewardMoney);

        if (isCompleteAllMission == false)
        {
            ExpManager.Instance.Add_Exp(rewardExp);
        }
        else
        {
            int bonus = 100;

            ExpManager.Instance.Add_Exp(rewardExp);
        }
    }
}

public class Mission
{
    public MissionManager.Type type;
    public int workCount;
    public int currWorkCount = 0;

    public UIMission uiMission;

    public Mission(MissionManager.Type _type, int _workCount = 1)
    {
        type = _type;
        workCount = _workCount;
    }

    public bool Progress()
    {
        currWorkCount++;

        uiMission.SetProgress((float)currWorkCount / workCount);
        UIMissionTable.I.ShowNotice();

        if (currWorkCount >= workCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
