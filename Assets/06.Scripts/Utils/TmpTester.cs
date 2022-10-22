using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpTester : MonoBehaviour
{
    public GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ss());
    }

    IEnumerator ss()
    {
        for (int i = 0; i < 10; i++)
        {
            Add();
            yield return new WaitForSeconds(1f);
        }
    }

    void Add()
    {
        GameObject c = Instantiate(child);
        c.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
