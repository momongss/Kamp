using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager I { get; private set; }

    public enum Type { Tent, Cook, None }
    public Type type;

    bool isDoingMission = false;

    public List<Dictionary<string, object>> mission_list;
    public List<Dictionary<string, object>> rewardMap_Exp;
    public List<Dictionary<string, object>> rewardMap_Money;

    public Transform Tr_missionUI_Parent;
    UIMission[] missionUIs;

    int max_day;

    public Type[][] missions;

    public int daycount = 16;

    public int currDay = 0;

    public Type[] Mission_Day0;
    public Type[] Mission_Day1;
    public Type[] Mission_Day2;
    public Type[] Mission_Day3;
    public Type[] Mission_Day4;
    public Type[] Mission_Day5;
    public Type[] Mission_Day6;
    public Type[] Mission_Day7;
    public Type[] Mission_Day8;
    public Type[] Mission_Day9;
    public Type[] Mission_Day10;
    public Type[] Mission_Day11;
    public Type[] Mission_Day12;
    public Type[] Mission_Day13;
    public Type[] Mission_Day14;
    public Type[] Mission_Day15;

    public UIMissionTable UI_MissionTable;

    int curr_mission_index = 0;

    private void Awake()
    {
        I = this;

        missionUIs = new UIMission[Tr_missionUI_Parent.childCount];

        for (int i = 0; i < Tr_missionUI_Parent.childCount; i++)
        {
            missionUIs[i] = Tr_missionUI_Parent.GetChild(i).GetComponent<UIMission>();

            if (Mission_Day1.Contains(missionUIs[i].missionType))
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

        missions = new Type[daycount][];
        missions[0] = Mission_Day0;
        missions[1] = Mission_Day1;
        missions[2] = Mission_Day2;
        missions[3] = Mission_Day3;
        missions[4] = Mission_Day4;
        missions[5] = Mission_Day5;
        missions[6] = Mission_Day6;
        missions[7] = Mission_Day7;
        missions[8] = Mission_Day8;
        missions[9] = Mission_Day9;
        missions[10] = Mission_Day10;
        missions[11] = Mission_Day11;
        missions[12] = Mission_Day12;
        missions[13] = Mission_Day13;
        missions[14] = Mission_Day14;
        missions[15] = Mission_Day15;

        UI_MissionTable.SetMissions(missions[currDay]);
    }

    public void StartMissionRoutines()
    {
        StartCoroutine(MissionStart(missions[currDay][curr_mission_index], 0f));
    }

    IEnumerator MissionStart(Type _type, float delay)
    {
        yield return new WaitForSeconds(delay);
        type = _type;
        isDoingMission = true;

        switch (_type)
        {
            case Type.Tent:
                UIPlayerNotice.I.ShowNotice("텐트를 치세요!!");
                break;
            case Type.Cook:
                UIPlayerNotice.I.ShowNotice("친구들을 위해 요리를 해요");
                break;
        }
    }

    public void OnMissionComplete(Type _type)
    {
        if (isDoingMission == false) return;
        if (type != _type) return;

        type = Type.None;
        isDoingMission = false;

        if (curr_mission_index < missions[currDay].Length - 1)
        {
            GiveReward(_type);

            curr_mission_index++;
            StartCoroutine(MissionStart(missions[currDay][curr_mission_index], 5f));
        }
        else
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
            ExpManager.Instance.Add_Exp(rewardExp, $"+{rewardExp}exp!!");
        }
        else
        {
            int bonus = 100;

            ExpManager.Instance.Add_Exp(rewardExp, $"+{rewardExp + bonus}exp (보너스 {bonus}exp) " +
                $"오늘 미션을 모두 완료했어요!! " +
                $"이제 편안히 캠핑을 즐겨봐요!");
        }
    }
}
