using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodPiece : MonoBehaviour
{
    bool isCutable = true;

    List<Transform> childs = new List<Transform>();
    Rigidbody rigid;

    Cuttable parentIngredient;

    private void OnTriggerEnter(Collider other)
    {
        if (transform.childCount > 1) return;

        if (other.CompareTag(Tag.Pot))
        {
            CookManager.Instance.OnCompleteWork(parentIngredient.foodType, Food.Action.PutInPot);
        }
    }

    private void Awake()
    {
        // FoodPiece 는 항상 Cuttable 자식으로 존재하는 걸로 가정한다.
        parentIngredient = transform.parent.GetComponent<Cuttable>();

        // Rigidbody 가 없으면 XRGrabInteractable 도 없는 것으로 간주한다.
        if (rigid == null)
        {
            gameObject.AddComponent<XRGrabInteractable>();
            rigid = gameObject.GetComponent<Rigidbody>();
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            childs.Add(transform.GetChild(i));
        }
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

        parentIngredient.Cut();
    }

    public void AddForce(Vector3 force)
    {
        // Rigidbody 가 없으면 XRGrabInteractable 도 없는 것으로 간주한다.
        if (rigid == null)
        {
            gameObject.AddComponent<XRGrabInteractable>();
            rigid = gameObject.GetComponent<Rigidbody>();
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

        return Piece.AddComponent<FoodPiece>();
    }
}
