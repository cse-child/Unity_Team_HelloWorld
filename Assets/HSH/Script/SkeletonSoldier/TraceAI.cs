using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.IO;

public class TraceAI : MonoBehaviour
{
    enum State
    {
        IDLE, TRACE, ATTACK, DEAD, HURT
    }

    public float traceRange = 5.0f;
    public float attackRange = 1.0f;

    public float moveSpeed = 0.5f;
    public float rotateSpeed = 0.5f;
    //private float maxHp = 100.0f;
    private float curHp = 100.0f;

    private GameObject target;
    private Animator animator;

    public GameObject itemPrefab;
    public System.Action onDie;

    private MonsterCloseAttack closeAtk;

    private State curState;

    private readonly WaitForSeconds delayTime = new WaitForSeconds(0.1f);
    private void Awake() //할당을 할 때 한번만 실행되는 Awake에서
    {
        animator = GetComponent<Animator>();
        closeAtk = GetComponent<MonsterCloseAttack>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start() // 여러번 실행될 수 있으므로 할당 x
    {
        StartCoroutine(SetState());
    }

    private void Update()
    {
        //rigidbody.velocity = Vector3.zero; // 물리적 가속도를 0으로 만드는 코드 이때 rigidbody의 Freeze Position은 해제상태로
        SetAction();
        Hurt();
        RotateMove();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, traceRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    private IEnumerator SetState()
    {
        while (curState != State.DEAD)
        {
            yield return delayTime;

            if (target == null)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < attackRange)
                curState = State.ATTACK;
            else if (distance < traceRange)
                curState = State.TRACE;
            else
                curState = State.IDLE;
        }
    }

    private void SetAction()
    {
        switch (curState)
        {
            case State.TRACE:
                Trace();
                break;
            case State.ATTACK:
                closeAtk.TryAttack();
                //Attack();
                break;
            case State.IDLE:
                Idle();
                break;
            case State.DEAD:
                Die();
                break;
        }
    }

    private void Trace()
    {
        if (animator.GetBool("isDie")) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Attack01")) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Idle01_Action01")) return;

        animator.SetBool("isAttack", false);
        animator.SetBool("isTrace", true);

        Vector3 direction = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
    }

    //private void Attack()
    //{
    //    animator.SetBool("isAttack", true);
    //    animator.SetBool("isTrace", false);
    //}
    private void EndAttack()
    {
        animator.SetBool("isAttack", false);
    }
    private void Idle()
    {
        animator.SetBool("isAttack", false);
        animator.SetBool("isTrace", false);
 
    }
    private void RotateMove()
    {
        if(animator.GetBool("isTrace")) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Idle01_Action01"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.902f)
            {
                Vector3 targetPosition = new Vector3(CalculateWanderPosition().x, 0, CalculateWanderPosition().z);
                Vector3 rotDir = targetPosition - transform.position;
                transform.rotation = Quaternion.LookRotation(rotDir);
                //천천히 변경
                //Quaternion rotation = Quaternion.LookRotation(rotDir);
                //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.01f);
                animator.SetTrigger("trigExploration");
            }
        }
    }

    private Vector3 CalculateWanderPosition()
    {
        float wanderRadius = 10.0f; // 현재 위치를 원점으로 하는 원의 반지름
        int wanderJitter = 0; // 선택된 각도 (wanderJitterMin ~ wanderJitterMax)
        int wanderJitterMin = 0; // 최소 각도
        int wanderJitterMax = 360; // 최대 각도

        //현재 적 캐릭터가 있는 월드의 중심 위치와 크기 (구역을 벗어난 행동 x)
        Vector3 rangePosition = Vector3.zero;
        Vector3 rangeScale = Vector3.one * 100.0f;

        //자신의 위치를 중심으로 반지름(wanderRadius) 거리, 선택된 각도(wanderJitter)에 위치한 좌표를 목표지점으로 설정
        wanderJitter = Random.Range(wanderJitterMin, wanderJitterMax);
        Vector3 targetPosition = transform.position + SetAngle(wanderRadius, wanderJitter);

        return targetPosition;
    }
    private Vector3 SetAngle(float radius, int angle)
    {
        Vector3 position = Vector3.zero;

        position.x = Mathf.Cos(angle) * radius;
        position.z = Mathf.Sin(angle) * radius;
        return position;
    }

    private void Hurt()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (animator.GetBool("isDie")) return;
            animator.SetTrigger("trigHurt");
            curHp -= 10.0f;
            animator.SetFloat("curHp", curHp);
            if (curHp <= 0)
            {
                animator.SetTrigger("trigDie");
                animator.SetBool("isDie", true);
                curState = State.DEAD;
            }
        }
    }
    private void Die()
    {
        this.DropItem();
        //yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        this.onDie();

    }
    public void DropItem()
    {
        var itemGo = Instantiate<GameObject>(this.itemPrefab);
        itemGo.transform.position = this.gameObject.transform.position;
        itemGo.SetActive(false);
        this.onDie = () =>
        {
            itemGo.SetActive(true);
            FarmingItem();
        };
    }
    private void FarmingItem()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/HSH/DataTable/" + "ItemData.csv");

        bool endOfFile = false;
        while (!endOfFile)
        {
            string dataString = sr.ReadLine();
            if (dataString == null)
            {
                endOfFile = true;
                break;
            }
            var dataValues = dataString.Split(',');
            for (int i = 0; i < dataValues.Length; i++)
            {
                Debug.Log("v: " + i.ToString() + " " + dataValues[i].ToString());
            }
        }
    }
}

