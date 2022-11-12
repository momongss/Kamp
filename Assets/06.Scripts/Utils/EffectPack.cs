using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPack : MonoBehaviour
{
    public static EffectPack I { get; private set; }

    private void Awake()
    {
        I = this;
    }

    public ParticleSystem PS_Smoke;

    [Header("Confetti")]
    public ParticleSystem Confetti_01;
    public ParticleSystem Confetti_02;
}
