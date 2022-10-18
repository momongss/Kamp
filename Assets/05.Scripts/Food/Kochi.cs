using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kochi : MonoBehaviour
{
    public Food.Type foodType;

    public Transform foodPosListParent;
    public Transform[] foodPosList;

    int currPos;

    public bool isStickOnAll = false;

    private void Awake()
    {
        foodPosList = Utils.GetChildren(foodPosListParent);

        currPos = 0;
    }

    public void StickOn(FoodPiece piece)
    {
        if (currPos >= foodPosList.Length) return;

        piece.transform.parent = foodPosList[currPos];
        currPos++;

        if (currPos == foodPosList.Length)
        {
            isStickOnAll = true;
        }

        MeshRenderer renderer = piece.GetComponentInChildren<MeshRenderer>();
        print(renderer);
    }
}
