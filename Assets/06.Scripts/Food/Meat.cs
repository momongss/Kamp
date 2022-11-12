using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : Food
{
    public float cookTime = 15f;
    public float currCookTime = 0f;

    public ParticleSystem PS_cookSmoke;
    public ParticleSystem PS_complete;

    public MeshRenderer meatRenderer;

    public Texture meatCookedTexture;

    Material mat;

    bool isCooking = false;

    Collider currFire;

    private void Start()
    {
        mat = meatRenderer.material;
    }

    void Update()
    {
        if (isCooking)
        {
            currCookTime += Time.deltaTime;

            if (!isCompleted && currCookTime >= cookTime)
            {
                mat.mainTexture = meatCookedTexture;

                isCompleted = true;
                CompletedCooking();
            }
        }

        // ���Ⱑ �׻� ���� ��������.
        if (PS_cookSmoke) PS_cookSmoke.transform.eulerAngles = Vector3.up;
    }

    void CompletedCooking()
    {
        print("�ϼ�!!");

        if (PS_cookSmoke) PS_cookSmoke.Stop();
        if (PS_complete) PS_complete.Play();

        CookManager.I.OnCompleteWork(this, Food.Action.Roast);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag(Tag.Fire))
        {
            print("Ÿ��Ÿ��");
            isCooking = true;
            currFire = other;

            if (PS_cookSmoke) PS_cookSmoke.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Fire) && other == currFire)
        {
            isCooking = false;
            currFire = null;

            if (PS_cookSmoke) PS_cookSmoke.Stop();
        }
    }
}
