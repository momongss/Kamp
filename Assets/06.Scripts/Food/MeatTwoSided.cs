using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatTwoSided : Food
{
    public float cookTime = 15f;
    public float currCookTimeUP = 0f;
    public float currCookTimeDown = 0f;

    public ParticleSystem PS_cookSmoke;
    public ParticleSystem PS_complete;

    public MeshRenderer meatUP;
    public MeshRenderer meatDown;
    public MeshRenderer meatSide;

    public Texture meatFaceTexture;
    public Texture meatSideTexture;

    Material MatUp;
    Material MatDown;
    Material MatSide;

    bool isCooked = false;

    bool isCompleted = false;
    bool isCompletedUp = false;
    bool isCompletedDown = false;

    Collider currFire;

    private void Start()
    {
        MatUp = meatUP.material;
        MatDown = meatDown.material;
        MatSide = meatSide.material;
    }

    void Update()
    {
        if (isCooked)
        {
            Collider coll = Utils.RayCast(transform.position, transform.TransformDirection(Vector3.up));

            if (coll == null)
            {
                // �Ʒ����� �Ʒ��� ����.
                currCookTimeDown += Time.deltaTime;

                if (!isCompletedDown && currCookTimeDown >= cookTime)
                {
                    MatDown.mainTexture = meatFaceTexture;
                    MatSide.mainTexture = meatSideTexture;

                    isCompletedDown = true;
                }
            }
            else
            {
                // ������ �Ʒ��� ����.
                currCookTimeUP += Time.deltaTime;

                if (!isCompletedUp && currCookTimeUP >= cookTime)
                {
                    MatUp.mainTexture = meatFaceTexture;
                    MatSide.mainTexture = meatSideTexture;

                    isCompletedUp = true;
                }
            }
        }

        if (!isCompleted && isCompletedUp && isCompletedDown)
        {
            isCompleted = true;
            CompletedCooking();
        }

        // ���Ⱑ �׻� ���� ��������.
        PS_cookSmoke.transform.eulerAngles = Vector3.up;
    }

    void CompletedCooking()
    {
        print("�ϼ�!!");
        PS_cookSmoke.Stop();
        PS_complete.Play();

        CookManager.Instance.OnCompleteWork(this, Food.Action.Roast);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(Tag.Fire))
        {
            isCooked = true;
            currFire = other;

            PS_cookSmoke.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Fire) && other == currFire)
        {
            isCooked = false;
            currFire = null;

            PS_cookSmoke.Stop();
        }
    }
}
