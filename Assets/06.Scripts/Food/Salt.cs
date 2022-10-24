using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salt : MonoBehaviour
{
    public Rigidbody rigid;
    public float sprinkle_force = 1f;

    public void Splinkle(Vector3 dir)
    {
        rigid.AddForce(dir * sprinkle_force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Pot))
        {
            print("소금소금");
        }
    }
}
