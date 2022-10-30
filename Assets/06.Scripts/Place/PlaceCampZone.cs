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
            CharacterManager.Instance.SetFreeCharacters();
            UIPlayerNotice.Instance.ShowNotice("캠핑을 시작해요!!", 4f);

            gameObject.SetActive(false);
        }
    }
}
