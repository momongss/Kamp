using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public enum Type
    {
        Multiple,

        Stew,
        Cookie,
        PorkBelly,
        Marshmellow,
        KoChi,
        Friedrice,

        Beef,
        Tomato,
        Onion,
        Carrot,
        Salt,
        BlackPepper,
        RedWine,

        Test01,
        Text02,
        GreenOnion,
        Chicken,
    };

    public enum Action
    {
        Cut,
        Roast,
        Skewer,
        Sprikle,

        PutInPot,
        PutInWok,

        Test01,
        Test02
    }

    public Type type;
    public static HashSet<Action> canPerformedMultiTimes = new HashSet<Action>() { Action.Cut, Action.Roast, Action.Skewer };
}
