using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowderBottle : MonoBehaviour
{
    public Powder salt;
    public ParticleSystem PS_Salt;

    public void Sprinkle()
    {
        Powder _salt = Instantiate(salt, PS_Salt.transform.position, Quaternion.Euler(transform.up));
        _salt.Splinkle(transform.up);

        PS_Salt.Play();
    }
}
