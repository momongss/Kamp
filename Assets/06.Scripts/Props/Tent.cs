using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Tent : MonoBehaviour
{
    public ParticleSystem PS_DustBurst;

    private void Start()
    {
        PS_DustBurst.Play();
        Destroy(PS_DustBurst.gameObject, 10f);
    }
}
