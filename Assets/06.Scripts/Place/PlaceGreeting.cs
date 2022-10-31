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
            UIPlayerNotice.I.ShowNotice("ĳ���͵��� �������!! ķ�������� �̵��ؿ�!!");

            PlaceCampZone.Instance.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
