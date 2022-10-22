using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Roastable : FoodPiece
{
    public float cookTime = 15f;
    public float currCookTime = 0f;

    public MeshRenderer meatRenderer;

    MeshFilter myMeshFilter;
    public MeshFilter cookedMeshFilter;
    Material mat;

    public bool isCooking = false;
    bool isCompleted = false;

    Collider currFire;

    UnityEvent cookCompleteEvent = new UnityEvent();

    protected override void Awake()
    {
        base.Awake();

        isRoastable = true;
        meatRenderer = GetComponentInChildren<MeshRenderer>();
        myMeshFilter = GetComponentInChildren<MeshFilter>();

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
                    case Food.Type.Beef:
                        // mat.mainTexture = TexturePack.Instance.meatDemoCooked;
                        // myMeshFilter.mesh.uv = TexturePack.Instance.Kochi_Meat_Cooked.mesh.uv;
                        break;
                    case Food.Type.KoChiMeat:
                        myMeshFilter.mesh = TexturePack.Instance.GetKochiMesh(myMeshFilter.mesh);
                        break;
                }

                isCompleted = true;
                CompletedCooking();
            }
        }
    }

    public void AddCookCompleteEvent(UnityAction action)
    {
        cookCompleteEvent.AddListener(action);
    }

    void CompletedCooking()
    {
        cookCompleteEvent.Invoke();
        CookManager.Instance.OnCompleteWork(this, Food.Action.Roast);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(Tag.Fire))
        {
            isCooking = true;
            currFire = other;
        }
    }

    protected void OnTriggerExit(Collider other)
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
