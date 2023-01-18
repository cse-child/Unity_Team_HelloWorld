using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.IO;
using Guirao.UltimateTextDamage;

public class BearAI : MonoBehaviour
{
    public UltimateTextDamageManager manager;
    public Transform trDamagePosition;
    private AudioSource audioSource;
    public enum State
    {
        IDLE, TRACE, ATTACK, DEAD, HURT
    }

    public float traceRange = 5.0f;
    public float attackRange = 1.0f;

    public float moveSpeed = 0.5f;
    public float rotateSpeed = 0.5f;
    //private float maxHp = 100.0f;
    private float curHp = 100.0f;
    private float Exp = 10.0f;
    private Vector3 curPos;
    public GameObject target;
    private Animator animator;

    public GameObject itemPrefab;
    public System.Action onDie;

    private BearCloseAttack closeAtk;
    private PlayerState playerState;

    public AudioClip audioHurt;
    public AudioClip audioDie;

    public State curState;

    private bool isTest = false;

    private readonly WaitForSeconds delayTime = new WaitForSeconds(0.1f);
    private void Awake() //할당을 할 때 한번만 실행되는 Awake에서
    {
        animator = GetComponent<Animator>();
        closeAtk = GetComponent<BearCloseAttack>();
        target = GameObject.FindGameObjectWithTag("Player");
        playerState = FindObjectOfType<PlayerState>();
        audioSource = GetComponent<AudioSource>();
        curPos = transform.position;
    }
    private void Start() // 여러번 실행될 수 있으므로 할당 x
    {
        StartCoroutine(SetState());
    }
    private void OnEnable()
    {
        StartCoroutine(SetState());
    }
    private void Update()
    {
        //rigidbody.velocity = Vector3.zero; // 물리적 가속도를 0으로 만드는 코드 이때 rigidbody의 Freeze Position은 해제상태로
        SetAction();
        if (Input.GetKeyDown(KeyCode.N))
        {
            print("N");
            Idle();
            isTest = true;
        }

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


            if (curHp <= 0)
            {
                curState = State.DEAD;
            }
        }
    }

    private void SetAction()
    {
        if (isTest) return;
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
                break;
        }
    }

    private void Trace()
    {
        if (animator.GetBool("isDie")) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Bear_GetHitFromFront")) return;
        animator.SetBool("isAttack", false);
        animator.SetBool("isTrace", true);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Bear_RunForward")) return;
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


    public void Hurt(float value)
    {
        if (animator.GetBool("isDie")) return;
        animator.SetTrigger("trigHurt");
        audioSource.clip = audioHurt;
        audioSource.Play();
        curHp -= value;
        MonsterUIManager.instance.SetMonster(curHp, 100, "야생 곰");
        MonsterUIManager.instance.SetActiveMonsterUI(true);
        manager.Add(value.ToString(), trDamagePosition, "default");
        animator.SetFloat("curHp", curHp);
        if (curHp <= 0)
        {
            animator.SetTrigger("trigDie");
            animator.SetBool("isDie", true);
            curState = State.DEAD;
            //Die();
        }
    }
    private void Die()
    {
        this.DropItem();
        this.onDie();

    }
    private IEnumerator DieAndRegen()
    {
        yield return new WaitForSeconds(3.0f);
        Die();
        gameObject.SetActive(false);
        curHp = 100.0f;
        animator.SetFloat("curHp", curHp);
        MonsterReSpawn.instance.ReSpawn(gameObject);
        curState = State.IDLE;
        transform.position = curPos;
        SetAction();
    }

    public void IncreaseExp(float value)
    {
        value += Exp;
    }
    private void DieAudio()
    {
        audioSource.clip = audioDie;
        audioSource.Play();
        StartCoroutine(DieAndRegen());
        MonsterUIManager.instance.SetActiveMonsterUI(false);
    }
    public void DropItem()
    {
        var itemGo = Instantiate<GameObject>(this.itemPrefab);
        itemGo.transform.position = this.gameObject.transform.position;
        itemGo.SetActive(false);
        this.onDie = () =>
        {
            itemGo.SetActive(true);
            playerState.IncreaseExp(Exp);
        };
    }
    public State GetState()
    {
        return curState;
    }
}

