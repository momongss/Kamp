using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject spawnItem;
    public float throwDistance = 3f;

    public bool isThrowed = false;

    void Spawn()
    {
        Instantiate(spawnItem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print($"coll {isThrowed} {collision.collider.CompareTag(Tag.Terrain)}");
        if (isThrowed && collision.collider.CompareTag(Tag.Terrain))
        {
            isThrowed = false;

            float distanceWithPlayer = Vector3.Distance(transform.position, Player.I.transform.position);
            print(distanceWithPlayer);

            if (distanceWithPlayer > throwDistance)
            {
                Spawn();
            }
        }
    }

    public void OnSelected()
    {

    }

    public void OnDeSelected()
    {
        isThrowed = true;
    }
}
