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

    protected override void Awake()
    {
        base.Awake();
        foodPosList = Utils.GetChildren(foodPosListParent);

        currPos = 0;
    }

    protected override void OnTriggerEnter(Collider other)
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

    public void OnGrabbed()
    {
        collisionColl.isTrigger = true;
    }

    public void OnUnGrabbed()
    {
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
