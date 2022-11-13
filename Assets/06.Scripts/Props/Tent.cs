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

        SquashNStretch squashNStretch = GetComponent<SquashNStretch>();
        squashNStretch.Squash_N_Stretch(1.2f, 0.7f, 1.2f);
    }
}
