using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCampZone : MonoBehaviour
{
    public static PlaceCampZone Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            CharacterManager.Instance.SetCharacters(Character.State.Idle);
            UIPlayerNotice.I.ShowNotice("���� ķ���� �����ؿ�!!", 3f);
            MissionManager.I.StartMissionRoutines();

            gameObject.SetActive(false);
        }
    }
}
