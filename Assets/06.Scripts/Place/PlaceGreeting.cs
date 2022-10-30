using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceGreeting : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            CampingManager.Instance.SpawnCharacters();
            UIPlayerNotice.Instance.ShowNotice("캐릭터들을 만났어요!! 캠핑장으로 이동해요!!");

            gameObject.SetActive(false);
        }
    }
}
