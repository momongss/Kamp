using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager I { get; private set; }

    public enum Type { Tent, Cook, None }
    public Type type;

    bool isDoingMission = false;

    public List<Dictionary<string, object>> rewardMap;

    int max_level;

    public Type[] Day1_missions;

    Type[] curr_Missions;
    int curr_mission_index = 0;

    private void Awake()
    {
        I = this;

        rewardMap = CSVReader.Read("RewardMap");

        max_level = rewardMap.Count - 1;

        curr_Missions = Day1_missions;
    }

    public void StartMissionRoutines()
    {
        StartCoroutine(MissionStart(curr_Missions[curr_mission_index], 0f));
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

        if (curr_mission_index < curr_Missions.Length - 1)
        {
            GiveReward(_type);

            curr_mission_index++;
            StartCoroutine(MissionStart(curr_Missions[curr_mission_index], 5f));
        }
        else
        {
            GiveReward(_type);

            CampingManager.Instance.OnCompleteMissionRoutines();
        }
    }

    void GiveReward(Type _type, bool isCompleteAllMission = false)
    {
        int level = ExpManager.Instance.Get_Level();
        if (level > max_level)
        {
            Debug.LogError($"만렙 이상의 레벨 발생. 만렙 : {max_level}, 발생한 레벨 : {level}");
        }

        string s_type = _type.ToString();

        int rewardExp = (int)rewardMap[level][s_type];

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
