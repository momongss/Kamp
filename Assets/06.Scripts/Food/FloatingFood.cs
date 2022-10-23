using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FloatingFood : MonoBehaviour
{
    public enum State { Up, Down };
    public State state = State.Down;

    Rigidbody rigid;
    public float default_floating_force = 9.4f;

    public float up_force = 0.5f;
    public float floating_delay = 8f;

    public Pot pot;
    public float up_position = 0.44f;
    public float down_position = 0.1f;

    public float loopDelay = 5f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        XRGrabInteractable g = GetComponent<XRGrabInteractable>();

        if (g != null) Destroy(g);

        StartCoroutine(Routine());
    }

    IEnumerator Routine()
    {
        while (true)
        {
            yield return new WaitForSeconds(loopDelay);

            state = (State)Random.Range(0, 1);
        }
    }

    float FloatOnSurface()
    {
        if (transform.position.y > pot.top.position.y)
        {
            return 0f;
        }
        else
        {
            return up_force;
        }
    }

    float SinkIn()
    {
        if (transform.position.y > pot.bottom.position.y)
        {
            return 0f;
        }
        else
        {
            return up_force;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 force = Vector3.zero;

        force.y += default_floating_force;

        switch (state)
        {
            case State.Up:
                force.y += FloatOnSurface();
                break;
            case State.Down:
                force.y += SinkIn();
                break;
        }

        force.x = Random.Range(-2f, 2f);
        force.z = Random.Range(-2f, 2f);

        rigid.AddForce(force);
    }
}
