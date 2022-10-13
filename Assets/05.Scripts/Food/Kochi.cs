using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kochi : MonoBehaviour
{
    public Transform foodPosListParent;

    public Transform[] foodPosList;

    int currPos;

    private void Awake()
    {
        foodPosList = Utils.GetChildren(foodPosListParent);

        currPos = 0;
    }

    public void StickOn(FoodPiece piece)
    {
        print("²ÅÀ½.");
        if (currPos >= foodPosList.Length) return;

        piece.transform.parent = foodPosList[currPos];
        currPos++;
    }
}
