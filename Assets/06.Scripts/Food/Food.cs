using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
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
        Sprikle = 3,

        PutInPot = 5,
        PutInWok = 6,
    }

    public Type type;
    public static HashSet<Action> canPerformedMultiTimes = new HashSet<Action>() { Action.Cut, Action.Roast, Action.Skewer };
    public static HashSet<Action> isRoastable = new HashSet<Action>() { };
}
