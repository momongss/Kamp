using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player I { get; private set; }

    public Transform spawnBallPoint;

    private void Awake()
    {
        I = this;
    }
}
