using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodPiece : Food
{
    public bool isCutable = true;
    public bool isRoastable = false;
    public bool isStickable = false;

    List<Transform> childs = new List<Transform>();
    XRGrabInteractable grabInteractable;
    SquashNStretch squashNStretch;

    Kochi kochi;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (transform.childCount > 1) return;

        if (other.CompareTag(Tag.Pot))
        {
            CookManager.Instance.OnCompleteWork(this, Action.PutInPot);
        }

        else if (other.CompareTag(Tag.Kochi))
        {
            kochi = other.GetComponent<Kochi>();

            print("꽂을려고 시도함.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tag.Kochi))
        {
            print("여기");
            if (kochi == other.GetComponent<Kochi>())
            {
                print("꽂을려다 말았음.");

                kochi = null;
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        if (grabInteractable == null)
        {
            InitComponents();
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            childs.Add(transform.GetChild(i));
        }
    }

    void InitComponents()
    {
        grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
        squashNStretch = gameObject.AddComponent<SquashNStretch>();

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

    public void StickedOn()
    {
        if (transform.childCount > 1) return;

        Destroy(grabInteractable);
        Destroy(rigid);

        CookManager.Instance.OnCompleteWork(this, Action.Skewer);
    }

    public void Cut(int pieceNum)
    {
        if (isCutable == true)
        {
            _Cut(pieceNum);
        }
    }

    void _Cut(int pieceNum)
    {
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
        // Rigidbody 가 없으면 XRGrabInteractable 도 없는 것으로 간주한다.
        if (rigid == null)
        {
            InitComponents();
        }
        rigid.AddForce(force);
    }

    FoodPiece MakeFoodPiece(List<Transform> pieces)
    {
        if (pieces.Count == 0)
        {
            Debug.LogWarning("커팅 로직 에러 위험");
            return null;
        }

        GameObject Piece = new GameObject("piece");

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

        FoodPiece foodPiece = foodPiecarize(Piece);
        foodPiece.foodType = foodType;

        if (pieces.Count == 1)
        {
            foodPiece.isStickable = true;
            foodPiece.transform.parent = null;

            CookManager.Instance.OnCompleteWork(this, Action.Cut);
        }

        foodPiece.foodType = foodType;

        return foodPiece;
    }

    protected virtual FoodPiece foodPiecarize(GameObject Piece)
    {
        return Piece.AddComponent<FoodPiece>();
    }
}
