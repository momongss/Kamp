using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Roastable : FoodPiece
{
    public float cookTime = 7f;
    public float currCookTime = 0f;

    public MeshRenderer meatRenderer;

    MeshFilter myMeshFilter;
    public MeshFilter cookedMeshFilter;
    Material mat;

    public bool isCooking = false;
    bool isCompleted = false;

    Collider currFire;

    ParticleSystem PS_Smoke_Instanced;

    UnityEvent cookCompleteEvent = new UnityEvent();

    protected override void Awake()
    {
        base.Awake();

        isRoastable = true;
        meatRenderer = GetComponentInChildren<MeshRenderer>();
        myMeshFilter = GetComponentInChildren<MeshFilter>();

        mat = meatRenderer.material;
    }

    private void Start()
    {
        PS_Smoke_Instanced = Instantiate(EffectPack.I.PS_Smoke, transform.GetChild(0));
        PS_Smoke_Instanced.transform.localPosition = Vector3.zero;
        PS_Smoke_Instanced.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
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

            if (PS_Smoke_Instanced) PS_Smoke_Instanced.Play();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Fire) && other == currFire)
        {
            isCooking = false;
            currFire = null;

            if (PS_Smoke_Instanced) PS_Smoke_Instanced.Stop();
        }
    }

    protected override FoodPiece foodPiecarize(GameObject Piece)
    {
        Roastable roastable = Piece.AddComponent<Roastable>();
        roastable.enabled = true;
        roastable.cookTime = cookTime;

        return roastable;
    }
}
