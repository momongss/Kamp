using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kochi : Food
{
    public Transform foodPosListParent;
    public Transform[] foodPosList;

    public Collider collisionColl;

    int currPos;

    public bool isStickOnAll = false;

    List<Roastable> roastableList = new List<Roastable>();

    bool isRoasted = false;

    FoodPiece currPiece;

    private void Awake()
    {
        foodPosList = Utils.GetChildren(foodPosListParent);

        currPos = 0;
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
        else if (other.CompareTag(Tag.FoodPiece))
        {
            currPiece = other.GetComponent<FoodPiece>();
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
        else if (other.CompareTag(Tag.FoodPiece))
        {
            currPiece = null;
        }
    }

    public void OnGrabbed()
    {
        collisionColl.isTrigger = true;
    }

    public void OnUnGrabbed()
    {
        if (currPiece == null) return;

        StickOn(currPiece);

        collisionColl.isTrigger = false;
    }

    public void StickOn(FoodPiece piece)
    {
        if (currPos >= foodPosList.Length) return;

        piece.StickedOn();

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

    void CompleteCooking()
    {
        if (isRoasted) return;
        isRoasted = true;

        CookManager.Instance.OnCompleteWork(this, Food.Action.Roast);
    }
}
