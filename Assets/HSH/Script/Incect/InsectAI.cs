using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectAI : MonoBehaviour
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
    public Transform target;
    private Animator animator;
    private Rigidbody rigidbody;

    public GameObject itemPrefab;
    public System.Action onDie;

    private State curState;

    private readonly WaitForSeconds delayTime = new WaitForSeconds(0.1f);
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(SetState());
    }

    private void Update()
    {
        if(Input.GetKeyDown("h"))
        {
            animator.SetTrigger("trigDie");
            animator.SetBool("isDie", true);
            curState = State.DEAD;
        }
        //rigidbody.velocity = Vector3.zero; // 물리적 가속도를 0으로 만드는 코드 이때 rigidbody의 Freeze Position은 해제상태로
        RotateMove();
        SetAction();
        Hurt();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, traceRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            rigidbody.transform.position = collision.transform.position;
        }
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

            float distance = Vector3.Distance(transform.position, target.position);
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
                Attack();
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Idle01_Action01")) return;
        if (animator.GetBool("isAttack") == true) return;
        animator.SetBool("isTrace", true);
        Vector3 direction = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
    }

    private void Attack()
    {
        animator.SetBool("isAttack", true);
        animator.SetBool("isTrace", false);
    }
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Idle01_Action01"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                float randY = Random.Range(-90, 90);
                Vector3 dir = new Vector3(0, randY, 0);
                Vector3 rotDir = transform.position - dir;
                transform.rotation = Quaternion.Euler(dir);
                return;
            }
        }
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
        };
    }
}
