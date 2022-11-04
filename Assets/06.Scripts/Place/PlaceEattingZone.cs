using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlaceEattingZone : MonoBehaviour
{
    public static PlaceEattingZone I { get; private set; }

    public Seat[] seatList;

    private void Awake()
    {
        I = this;

        Transform[] T_seatList = Utils.GetChildren(transform);
        seatList = new Seat[T_seatList.Length];

        for (int i = 0; i < T_seatList.Length; i++)
        {
            seatList[i] = T_seatList[i].GetComponent<Seat>();
        }
    }

    public Seat GetSeat()
    {
        Seat seat = GetAvailableSeatIndex();

        return seat;
    }

    public void ReturnSeat(Seat seat)
    {
        seat.UnSit();
    }

    public Seat GetAvailableSeatIndex()
    {
        foreach (Seat s in seatList)
        {
            if (s.isSeated == false)
            {
                return s;
            }
        }

        return null;
    }
}
