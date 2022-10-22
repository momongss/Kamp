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

    int totalRoastableCount = 0;
    int cookedRoastableCount = 0;

    List<Roastable> roastableList = new List<Roastable>();

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

        totalRoastableCount++;

        if (piece.isRoastable)
        {
            Roastable roastable = (Roastable)piece;
            roastable.AddCookCompleteEvent(() =>
            {
                cookedRoastableCount++;
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
        print("꼬치가 구워졌다");
        CookManager.Instance.OnCompleteWork(Food.Type.KoChi, Food.Action.Roast);
    }
}
