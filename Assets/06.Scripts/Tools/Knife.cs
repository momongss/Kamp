using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.FoodCutTrigger))
        {
            FoodPiece piece = other.transform.parent.parent.GetComponent<FoodPiece>();

            int pieceNum = Int32.Parse(other.name) + 1;

            Destroy(other.gameObject);
            piece.Cut(pieceNum);
        }
    }
}
