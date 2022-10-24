using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powder : Food
{
    public float sprinkle_force = 1f;

    public void Splinkle(Vector3 dir)
    {
        rigid.AddForce(dir * sprinkle_force);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(Tag.Pot))
        {
            CookManager.Instance.OnCompleteWork(this, Action.PutInPot);

            Destroy(gameObject);
        }
    }
}
