using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool hasRigidbody = true;
    public Rigidbody rigid;

    public int satiety = 10;

    public bool isCompleted = false;
    public bool isGived = false;

    protected virtual void Awake()
    {
        if (hasRigidbody && rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
            if (rigid == null)
            {
                rigid = gameObject.AddComponent<Rigidbody>();
            }
        }
    }

    bool hasFloatingFood = false;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Pot) && hasFloatingFood == false)
        {
            hasFloatingFood = true;

            FloatingFood _f = gameObject.AddComponent<FloatingFood>();
            _f.pot = other.GetComponent<Pot>(); ;
        }

        else if (other.CompareTag(Tag.Stew))
        {
            Stew stew = other.GetComponent<Stew>();
            stew.PutFood(this);
        }

        if (isCompleted && !isGived)
        {
            if (other.gameObject.layer == Layer.Character)
            {
                isGived = true;
                Character character = other.GetComponent<Character>();
                character.GiveFood(this);
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public enum Type
    {
        Multiple = 0,

        Stew = 1,
        Cookie = 2,
        PorkBelly = 3,
        Marshmellow = 4,
        KoChi = 5,
        Friedrice = 6,

        Beef = 7,
        Tomato = 8,
        Onion = 9,
        Carrot = 10,

        Salt = 11,
        BlackPepper = 12,
        RedWine = 13,

        Test01 = 14,
        Text02 = 15,

        GreenOnion = 16,
        Chicken = 17,

        KoChiMeat = 18,
    };

    public enum Action
    {
        Cut = 0,
        Roast = 1,
        Skewer = 2,
        Sprinkle = 3,

        PutInPot = 5,
        PutInWok = 6,
    }

    public Type foodType;
    public static HashSet<Action> canPerformedMultiTimes = new HashSet<Action>() { Action.Cut, Action.Roast, Action.Skewer };
}
