using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceAI : MonoBehaviour
{
    enum State
    {
        IDLE, TRACE, ATTACK, DEAD
    }

    public float traceRange = 5.0f;
    public float attackRange = 1.0f;

    public float moveSpeed = 0.5f;
    public float rotateSpeed = 0.5f;
    public Transform target;
    private Animator animator;
    private State curState;

    private readonly WaitForSeconds delayTime = new WaitForSeconds(0.1f);
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SetState());
    }

    private void Update()
    {
        RotateMove();
        SetAction();
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
<<<<<<< Updated upstream

            case State.IDLE:
                Idle();
                break;
=======
>>>>>>> Stashed changes
        }
    }

    private void Trace()
    {
        if (animator.GetBool("isAttack") == true) return;
        animator.SetBool("isTrace", true);
        Vector3 direction = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
    }
<<<<<<< Updated upstream

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
=======
    private void Attack()
    {
>>>>>>> Stashed changes
    }
}
