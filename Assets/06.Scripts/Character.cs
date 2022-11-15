using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public enum Type
    {
        Penguin = 0,
        Sheep = 1,
        Duck = 2,
        Cat = 3,
        GGUM = 4,

        None = -1
    }

    public Type type;

    public enum State
    {
        Idle,
        ArrivalToCampground,
        FollowPlayer,
        WalkAround,
        MoveToPlayer,
        TalkToPlayer,
        GotoEat,
        WaitMeal
    }
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

    public ParticleSystem PS_Appear;
    public ParticleSystem PS_Heart;

    public string[] talk_anything = new string[] {
        "����",
        "�ʹ� ���ڴ�~",
        "�ʹ� �ų�!",
    };

    public string[] talk_special = new string[] { };

    Seat seat;

    public void OnSelected()
    {
        if ((state == State.Idle || state == State.WalkAround) == false) return;

        squashNStretch.Squash_N_Stretch(1.15f, 0.85f, 1.15f);

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

    public void GiveFood(Food food)
    {
        PS_Heart.Play();

        speechBubble.Talk("�ȳ�.. ����!!", 4f);

        MissionManager.I.OnMissionComplete(MissionManager.Type.Cook);

        ChangeState(State.WalkAround);

        Destroy(food.gameObject);
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
            Debug.LogWarning("�н� ��� 100ȸ �Ѿ. ���� ������ �־��.");
        }

        dest.x += transform.position.x;
        dest.z += transform.position.z;
        agent.SetDestination(dest);
    }

    public void ChangeState(State _state)
    {
        state = _state;

        stateTimer = Time.time;

        switch (_state)
        {
            case State.Idle:
                SetAgentWalkable(false);
                animator.SetTrigger("IDLE");
                animator.ResetTrigger("WALK");
                StartCoroutine(SayAnything());
                break;

            case State.ArrivalToCampground:
                string[] talk_Candidates = CampingManager.Instance.speach_Arrival_To_Camp;
                if (talk_Candidates.Length == 0)
                {
                    Debug.LogWarning("Camping Manager�� ��� �ȳ־��.");
                    break;
                }
                string talk = talk_Candidates[Random.Range(0, talk_Candidates.Length)];
                speechBubble.Talk(talk, 6f);
                transform.LookAt(Player.I.transform);

                PS_Appear.Play();
                Destroy(PS_Appear.gameObject, 5f);
                break;

            case State.FollowPlayer:
                SetAgentWalkable(true);
                animator.ResetTrigger("IDLE");
                animator.SetTrigger("WALK");

                Vector3 delta = (transform.position - Player.I.transform.position).normalized * 3f;

                SetMoveDest(Player.I.transform.position - delta);
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
                speechBubble.Talk("�ȳ�?? ����");
                break;
            case State.GotoEat:
                SetAgentWalkable(true);
                animator.ResetTrigger("IDLE");
                animator.SetTrigger("WALK");
                seat = PlaceEattingZone.I.GetSeat();
                SetMoveDest(seat.transform.position);
                break;
            case State.WaitMeal:
                transform.LookAt(Player.I.transform);
                SetAgentWalkable(false);
                animator.SetTrigger("IDLE");
                animator.ResetTrigger("WALK");
                break;
        }
    }

    IEnumerator SayAnything()
    {
        while (state == State.Idle || state == State.WalkAround)
        {
            yield return new WaitForSeconds(Random.Range(7f, 14f));

            string talk;
            if (talk_special.Length > 0)
            {
                if (Random.Range(0f, 1f) > 0.7f)
                {
                    speechBubble.Talk(talk_special[0]);
                    yield return new WaitForSeconds(Random.Range(6f, 7f));
                    speechBubble.Talk(talk_special[1]);
                }
                else
                {
                    talk = talk_anything[Random.Range(0, talk_anything.Length)];
                    speechBubble.Talk(talk);
                }
            }
            else
            {
                talk = talk_anything[Random.Range(0, talk_anything.Length)];
                speechBubble.Talk(talk);
            }
        }
    }

    void Update()
    {
        float curTime = Time.time;

        switch (state)
        {
            case State.Idle:

                break;

            case State.ArrivalToCampground:
                break;

            case State.FollowPlayer:
                Vector3 delta = (transform.position - Player.I.transform.position).normalized * 3f;

                SetMoveDest(Player.I.transform.position);
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

            case State.GotoEat:
                if (agent.pathPending == false && agent.remainingDistance <= agent.stoppingDistance)
                {
                    ChangeState(State.WaitMeal);
                }
                break;
            case State.WaitMeal:

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
        speechBubble.gameObject.SetActive(false);
    }

    void Start()
    {
        CharacterManager.I.RegisterCharacter(this);

        navPath = new NavMeshPath();

        ChangeState(State.ArrivalToCampground);
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
            case State.WaitMeal:
                if (other.CompareTag(Tag.Food))
                {

                }
                break;
        }
    }

    IEnumerator BonfireRoutine(Transform bonfire)
    {
        ChangeState(State.Idle);

        transform.LookAt(bonfire);

        yield return new WaitForSeconds(1f);

        speechBubble.Talk("�����ϴ�..", 8f);

        yield return new WaitForSeconds(10f);

        ChangeState(State.WalkAround);
    }
}
