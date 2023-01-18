using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.IO;
using Guirao.UltimateTextDamage;

public class DragonAI : MonoBehaviour
{
    public UltimateTextDamageManager manager;
    public Transform trDamagePosition;
    private AudioSource audioSource;
    public Transform breathPort;
    public enum State
    {
        IDLE, TRACE, ATTACK, DEAD, HURT
    }

    public float traceRange = 15.0f;
    public float attackRange = 5.0f;
    public float moveSpeed = 2.0f;
    public float rotateSpeed = 0.5f;
    //private float maxHp = 100.0f;
    private float curHp = 100.0f;
    private float Exp = 100.0f;
    private Vector3 randPos;

    private GameObject target;
    private Animator animator;
    public GameObject itemPrefab;
    public GameObject CastPrefab;
    public GameObject CannonPrefab;

    public System.Action onDie;

    private DragonAttack closeAtk;

    public State curState;

    private readonly WaitForSeconds delayTime = new WaitForSeconds(0.1f);

    private PlayerState playerState;
    private PlayerControl playerControl;
    private RaycastHit hitInfo; // 현재 무기에 닿은 오브젝트 정보
    public LayerMask layerMask;
    Vector3 control = new Vector3(0, 0, 0);
    public AudioClip audioHurt;
    public AudioClip audioDie;
    public AudioClip audioBite;
    public AudioClip audioCast;
    public AudioClip audioBreath;

    private void Awake() //할당을 할 때 한번만 실행되는 Awake에서
    {
        animator = GetComponent<Animator>();
        closeAtk = GetComponent<DragonAttack>();
        target = GameObject.FindGameObjectWithTag("Player");
        playerState = FindObjectOfType<PlayerState>();
        playerControl = FindObjectOfType<PlayerControl>();
    }
    private void Start() // 여러번 실행될 수 있으므로 할당 x
    {
        StartCoroutine(SetState());
    }

    private void Update()
    {
        //rigidbody.velocity = Vector3.zero; // 물리적 가속도를 0으로 만드는 코드 이때 rigidbody의 Freeze Position은 해제상태로
        SetAction();
        Test();
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
                {
                    Trace();
                    DragonThink();
                }
                break;
            case State.ATTACK:
                audioSource.clip = audioBite;
                audioSource.Play();
                closeAtk.TryAttack();
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
        animator.SetBool("isAttack", false);
        animator.SetBool("isTrace", true);
        Vector3 direction = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    //private void Attack()
    //{
    //    animator.SetBool("isAttack", true);
    //    animator.SetBool("isTrace", false);
    //}
    private void EndAttack()
    {
    }
    private void Idle()
    {
        animator.SetBool("isAttack", false);
        animator.SetBool("isTrace", false);
    }

    private void Hurt(float value)
    {
        if (animator.GetBool("isDie")) return;
        animator.SetTrigger("trigHurt");
        curHp -= value;
        animator.SetFloat("curHp", curHp);
        if (curHp <= 0)
        {
            animator.SetTrigger("trigDie");
            animator.SetBool("isDie", true);
            curState = State.DEAD;
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
    private void DieAudio()
    {
        audioSource.clip = audioDie;
        audioSource.Play();
        MonsterUIManager.instance.SetActiveMonsterUI(false);
    }

    private void DragonThink()
    {
        int ranAction = Random.Range(0, 2);
        switch (ranAction)
        {
            case 0:
                animator.SetTrigger("trigBreath");
                break;
            case 1:
                animator.SetTrigger("trigCastSpell");
                break;
        }
    }
    private IEnumerator Breath()
    {
        audioSource.clip = audioBreath;
        audioSource.Play();
        var cannon = Instantiate<GameObject>(this.CannonPrefab);
        cannon.transform.position = breathPort.position;
        cannon.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        cannon.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        Destroy(cannon);
    }
    private IEnumerator Cast()
    {
        audioSource.clip = audioCast;
        audioSource.Play();
        float randx, randy;
        randx = Random.Range(-4, 4);
        randy = Random.Range(-2, 3);
        randPos = new Vector3(randx, randy, 0);
        var cast = Instantiate<GameObject>(this.CastPrefab);
        cast.transform.position = breathPort.transform.position + randPos;
        cast.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        cast.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        Destroy(cast);
    }

    private void Test()
    {
        if(Input.GetKeyDown(KeyCode.F1))
            animator.SetTrigger("trigBreath");

        if (Input.GetKeyDown(KeyCode.F2))
            animator.SetTrigger("trigCastSpell");
    }
    public State GetState()
    {
        return curState;
    }
}

