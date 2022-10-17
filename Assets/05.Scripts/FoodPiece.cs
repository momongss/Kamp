using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodPiece : MonoBehaviour
{
    bool isCutable = true;

    public bool isStickable = false;

    List<Transform> childs = new List<Transform>();
    Rigidbody rigid;
    XRGrabInteractable grabInteractable;

    Cuttable parentIngredient;

    Kochi kochi;

    Food.Type foodType;

    private void OnTriggerEnter(Collider other)
    {
        if (transform.childCount > 1) return;

        if (other.CompareTag(Tag.Pot))
        {
            CookManager.Instance.OnCompleteWork(foodType, Food.Action.PutInPot);
        }

        if (other.CompareTag(Tag.Kochi))
        {
            kochi = other.GetComponent<Kochi>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Kochi))
        {
            if (kochi == other.GetComponent<Kochi>())
            {
                kochi = null;
            }
        }
    }

    private void Awake()
    {
        // FoodPiece �� �ڸ� �� �ִٸ�(piece �� 1�� �̻�) Cuttable �ڽ����� �����ϴ� �ɷ� �����Ѵ�.
        if (transform.childCount > 1)
        {
            parentIngredient = transform.parent.GetComponent<Cuttable>();
            foodType = parentIngredient.foodType;
        }


        // Rigidbody �� ������ XRGrabInteractable �� ���� ������ �����Ѵ�.
        if (rigid == null)
        {
            InitRigid();
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            childs.Add(transform.GetChild(i));
        }
    }

    void InitRigid()
    {
        grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
        rigid = gameObject.GetComponent<Rigidbody>();

        SelectEnterEvent activated = new SelectEnterEvent();
        activated.AddListener((e) =>
        {
            OnGrabed();
        });
        grabInteractable.selectEntered = activated;

        SelectExitEvent deactivated = new SelectExitEvent();
        deactivated.AddListener((e) =>
        {
            OnUnGrabed();
        });
        grabInteractable.selectExited = deactivated;
    }

    void OnGrabed()
    {

    }

    void OnUnGrabed()
    {
        if (kochi == null) return;

        Destroy(grabInteractable);
        Destroy(rigid);

        kochi.StickOn(this);
        kochi = null;
    }

    public void Cut(int pieceNum)
    {
        if (!isCutable) return;

        isCutable = false;

        XRGrabInteractable xrGrab = gameObject.GetComponent<XRGrabInteractable>();
        Rigidbody _rigid = gameObject.GetComponent<Rigidbody>();

        Destroy(xrGrab);
        Destroy(_rigid);

        List<Transform> cLeft = childs.GetRange(0, pieceNum);
        List<Transform> cRight = childs.GetRange(pieceNum, childs.Count - pieceNum);

        FoodPiece fpLeft = MakeFoodPiece(cLeft);
        FoodPiece fpRight = MakeFoodPiece(cRight);

        if (cLeft.Count < cRight.Count)
        {
            fpLeft.AddForce((fpLeft.transform.position - fpRight.transform.position).normalized * 110f);
        }
        else
        {
            fpRight.AddForce((fpRight.transform.position - fpLeft.transform.position).normalized * 110f);
        }

        Destroy(gameObject);
    }

    public void AddForce(Vector3 force)
    {
        // Rigidbody �� ������ XRGrabInteractable �� ���� ������ �����Ѵ�.
        if (rigid == null)
        {
            InitRigid();
        }
        rigid.AddForce(force);
    }

    FoodPiece MakeFoodPiece(List<Transform> pieces)
    {
        if (pieces.Count == 0)
        {
            Debug.LogWarning("Ŀ�� ���� ���� ����");
            return null;
        }

        GameObject Piece = new GameObject("piece");
        Piece.transform.parent = parentIngredient.transform;
        Piece.transform.position = pieces[0].position;
        for (int i = 0; i < pieces.Count; i++)
        {
            var p = pieces[i];
            p.parent = Piece.transform;
            p.name = $"{i}";
            if (p.childCount > 0)
            {
                p.GetChild(0).name = $"{i}";
            }
        }

        FoodPiece foodPiece = Piece.AddComponent<FoodPiece>();

        if (pieces.Count == 1)
        {
            foodPiece.isStickable = true;
            foodPiece.transform.parent = null;

            CookManager.Instance.OnCompleteWork(foodType, Food.Action.Cut);
        }

        foodPiece.foodType = foodType;

        return foodPiece;
    }
}
