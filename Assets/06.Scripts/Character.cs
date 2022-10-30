using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public enum State { Idle, WalkAround, MoveToPlayer, TalkToPlayer }
    public State state = State.Idle;

    Transform model;
    Animator animator;

    NavMeshAgent agent;
    NavMeshPath navPath;

    public float stateIntervalIdle = 5f;
    public float turnInterval = 5f;
    public float WalkAroundArea = 5f;

    [Header("UI")]
    SpeechBubble speechBubble;

    Vector3 dest;

    float prevBonfireTime = -40f;
    float stateTimer = 0f;

    public GameObject testCube;

    Rigidbody rigid;

    SquashNStretch squashNStretch;

    public void OnSelected()
    {
        squashNStretch.Squash_N_Stretch(1.4f, 0.7f, 1.4f);

        float distance = Vector3.Distance(transform.position, Player.I.transform.position);

        if (distance > 2f)
        {
            ChangeState(State.MoveToPlayer);
        }
        else
        {
            ChangeState(State.TalkToPlayer);
            transform.LookAt(Player.I.transform);
        }
    }

    void SetAgentWalkable(bool isActive)
    {
        agent.updateRotation = isActive;
        agent.isStopped = !isActive;

        rigid.freezeRotation = !isActive;
    }

    void SetMoveDest(Vector3 _dest)
    {
        dest = _dest;
        agent.SetDestination(dest);
    }

    void SetRandomMoveDest()
    {
        int destCalCount = 0;

        do
        {
            dest = new Vector3(
                Random.Range(-WalkAroundArea, WalkAroundArea),
                0,
                Random.Range(-WalkAroundArea, WalkAroundArea));

            destCalCount++;
        } while (!agent.CalculatePath(dest, navPath) && destCalCount < 100);

        if (destCalCount >= 100)
        {
            Debug.LogWarning("패스 계산 100회 넘어감. 뭔가 문제가 있어보임.");
        }

        dest.x += transform.position.x;
        dest.z += transform.position.z;
        agent.SetDestination(dest);
    }

    void ChangeState(State _state)
    {
        state = _state;

        stateTimer = Time.time;

        switch (_state)
        {
            case State.Idle:
                SetAgentWalkable(false);
                animator.SetTrigger("IDLE");
                animator.ResetTrigger("WALK");
                break;
            case State.WalkAround:
                SetAgentWalkable(true);
                animator.ResetTrigger("IDLE");
                animator.SetTrigger("WALK");
                SetRandomMoveDest();
                break;
            case State.MoveToPlayer:
                SetAgentWalkable(true);
                animator.ResetTrigger("IDLE");
                animator.SetTrigger("WALK");
                SetMoveDest(Player.I.transform.position);
                break;
            case State.TalkToPlayer:
                SetAgentWalkable(false);
                animator.SetTrigger("IDLE");
                animator.ResetTrigger("WALK");
                speechBubble.Talk("안녕?? ㅎㅎ");
                break;
        }
    }

    public void PopUpSpeechBubble(string text)
    {
        speechBubble.Talk(text);
    }


    void Update()
    {
        float curTime = Time.time;

        switch (state)
        {
            case State.Idle:
                if (curTime - stateTimer > stateIntervalIdle)
                {
                    ChangeState(State.WalkAround);
                }

                break;
            case State.WalkAround:
                if (dest != null)
                {
                    float distance = agent.remainingDistance;

                    if (distance < 0.1f)
                    {
                        float rd = Random.Range(0f, 1f);
                        if (rd > 0.5f) ChangeState(State.Idle);
                        else ChangeState(State.WalkAround);
                    }
                    else if (curTime - stateTimer > turnInterval)
                    {
                        ChangeState(State.WalkAround);
                    }
                }
                break;
            case State.MoveToPlayer:
                break;
            case State.TalkToPlayer:
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        model = transform.GetChild(0);

        animator = model.GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);

        rigid = GetComponent<Rigidbody>();

        squashNStretch = GetComponent<SquashNStretch>();

        speechBubble = GetComponentInChildren<SpeechBubble>();
    }

    void Start()
    {
        navPath = new NavMeshPath();

        ChangeState(State.Idle);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.WalkAround:
                if (other.CompareTag(Tag.BonFireAround))
                {
                    float currTime = Time.time;
                    if (currTime - prevBonfireTime > 40f)
                    {
                        prevBonfireTime = currTime;
                        StartCoroutine(BonfireRoutine(other.transform));
                    }
                }

                break;
            case State.MoveToPlayer:
                if (other.CompareTag(Tag.Player))
                {
                    transform.LookAt(Player.I.transform);
                    ChangeState(State.TalkToPlayer);
                }
                break;
            case State.TalkToPlayer:
                break;
        }
    }

    IEnumerator BonfireRoutine(Transform bonfire)
    {
        ChangeState(State.Idle);

        transform.LookAt(bonfire);

        yield return new WaitForSeconds(1f);

        speechBubble.Talk("따뜻하다..", 8f);

        yield return new WaitForSeconds(10f);

        ChangeState(State.WalkAround);
    }
}
