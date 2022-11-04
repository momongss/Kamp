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
                UIPlayerNotice.I.ShowNotice("��Ʈ�� ġ����!!");
                break;
            case Type.Cook:
                UIPlayerNotice.I.ShowNotice("ģ������ ���� �丮�� �ؿ�");
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
            Debug.LogError($"���� �̻��� ���� �߻�. ���� : {max_level}, �߻��� ���� : {level}");
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

            ExpManager.Instance.Add_Exp(rewardExp, $"+{rewardExp + bonus}exp (���ʽ� {bonus}exp) " +
                $"���� �̼��� ��� �Ϸ��߾��!! " +
                $"���� ����� ķ���� ��ܺ���!");
        }
    }
}
