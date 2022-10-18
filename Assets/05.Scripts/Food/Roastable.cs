using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roastable : FoodPiece
{
    public float cookTime = 15f;
    public float currCookTime = 0f;

    public MeshRenderer meatRenderer;

    bool isCooking = false;
    bool isCompleted = false;

    Material mat;
    Collider currFire;

    protected override void Awake()
    {
        base.Awake();

        meatRenderer = GetComponentInChildren<MeshRenderer>();
        mat = meatRenderer.material;
    }

    void Update()
    {
        if (isCooking)
        {
            currCookTime += Time.deltaTime;

            if (!isCompleted && currCookTime >= cookTime)
            {
                switch (foodType)
                {
                    case Food.Type.Test01:
                        mat.mainTexture = TexturePack.Instance.meatDemoCooked;
                        break;
                }

                isCompleted = true;
                CompletedCooking();
            }
        }
    }

    void CompletedCooking()
    {
        print("¿Ï¼º!!");

        CookManager.Instance.OnCompleteWork(foodType, Food.Action.Roast);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Fire))
        {
            print("Å¸´ÚÅ¸´Ú");
            isCooking = true;
            currFire = other;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Fire) && other == currFire)
        {
            isCooking = false;
            currFire = null;
        }
    }

    protected override FoodPiece foodPiecarize(GameObject Piece)
    {
        return Piece.AddComponent<Roastable>();
    }
}
