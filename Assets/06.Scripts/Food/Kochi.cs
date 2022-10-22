using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kochi : Food
{
    public Transform foodPosListParent;
    public Transform[] foodPosList;

    int currPos;

    public bool isStickOnAll = false;

    List<Roastable> roastableList = new List<Roastable>();

    bool isRoasted = false;

    private void Awake()
    {
        foodPosList = Utils.GetChildren(foodPosListParent);

        currPos = 0;
    }

    public void StickOn(FoodPiece piece)
    {
        print(piece);

        if (currPos >= foodPosList.Length) return;

        piece.transform.parent = foodPosList[currPos];
        currPos++;

        if (currPos == foodPosList.Length)
        {
            isStickOnAll = true;
        }

        if (piece.isRoastable)
        {
            Roastable roastable = (Roastable)piece;
            roastable.AddCookCompleteEvent(() =>
            {
                CompleteCooking();
            });

            roastableList.Add(roastable);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Fire))
        {
            foreach (var r in roastableList)
            {
                r.isCooking = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Fire))
        {
            foreach (var r in roastableList)
            {
                r.isCooking = false;
            }
        }
    }

    void CompleteCooking()
    {
        if (isRoasted) return;
        isRoasted = true;

        CookManager.Instance.OnCompleteWork(this, Food.Action.Roast);
    }
}
