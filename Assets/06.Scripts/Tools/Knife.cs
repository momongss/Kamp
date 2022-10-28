using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    GameObject CutTrigger;
    FoodPiece foodPiece;

    int pieceNum;

    public float offesetY = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.FoodCutTrigger))
        {
            foodPiece = other.transform.parent.parent.GetComponent<FoodPiece>();

            pieceNum = Int32.Parse(other.name) + 1;

            // Destroy(CutTrigger);
            foodPiece.Cut(pieceNum);
        }
    }
}
