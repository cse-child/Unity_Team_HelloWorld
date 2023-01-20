using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.IO;
using Guirao.UltimateTextDamage;

public class DragonAI : MonoBehaviour
{
    public UltimateTextDamageManager manager; //������ ����
    public Transform trDamagePosition; // ������ ���� ���� Pos
    private AudioSource audioSource; // ����

    public Transform breathPort;// �극�� ���� Pos
    public enum State // FSM
    {
        IDLE, TRACE, ATTACK, DEAD, HURT
    }
    private enum AttackPattern
    {
        BITE, BREATH, CAST, GUARD, FALLING
    }
    public State curState; //�������
    public State curAttackPattern;

    public float traceRange = 18.0f; //�νĹ���
    public float attackRange = 10.0f; // ���� ����
    private float biteRange = 4.0f;
    public float moveSpeed = 2.0f; // �̵��ӵ�
    public float rotateSpeed = 0.5f; //������ȯ �ӵ�
    private int maxHp = 100; //�ִ�ü��
    private float curHp = 1000.0f; // ����ü��
    private float Exp = 1000.0f; // ��� �� ��� ����ġ

    private Vector3 randPos;

    private GameObject target; // �÷��̾� �±�
    private Animator animator; // ����� �ִϸ��̼�
    public GameObject itemPrefab; // ��� �� ��� ������ ������
    public GameObject CastPrefab; // ��ų ��� �� �����Ǵ� ������Ʈ
    public GameObject CannonPrefab; // ��ų ��� �� �����Ǵ� ������Ʈ 2

    public System.Action onDie; //����� ��� �Լ�

    private readonly WaitForSeconds delayTime = new WaitForSeconds(1.0f); // ������ Ÿ��

    private CapsuleCollider myCollider;
    private PlayerState playerState; //�÷��̾� ���� ����
    private PlayerControl playerControl; // �÷��̾� �ǰݱ��� �ʿ� ����
    private RaycastHit hitInfo; // ���� ���⿡ ���� ������Ʈ ����
    public LayerMask layerMask; // ���̾ƿ� �� �÷��̾� ���̾� ����
    Vector3 control = new Vector3(0, 0, 0);
    public AudioClip audioHurt;   // ��� ����
    public AudioClip audioDie;    // ��� ����
    public AudioClip audioBite;   // ��� ����
    public AudioClip audioCast;   // ��� ����
    public AudioClip audioBreath; // ��� ����

    private void Awake() //�Ҵ��� �� �� �ѹ��� ����Ǵ� Awake����
    {
        animator = GetComponent<Animator>(); // �ִϸ��̼�
        target = GameObject.FindGameObjectWithTag("Player"); //�÷��̾� Ÿ�� ����
        playerState = FindObjectOfType<PlayerState>(); // �÷��̾� ���ݽ� hp���ٿ�
        playerControl = FindObjectOfType<PlayerControl>(); // �ǰ� �� �÷��̾��� attack��ŭ �ǰ� �پ�� ����
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, traceRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Start()
    {
        StartCoroutine(SetState());
        StartCoroutine(StartPattern());
    }

    private void Update()
    {
        LookPlayer();
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

            if (curHp < 0)
                curState = State.DEAD;
        }
    }
    private void SetAction()
    {
        switch (curState)
        {
            case State.TRACE:
                break;
            case State.ATTACK:
                PlayAttackPattern();
                break;
            case State.IDLE:
                break;
            case State.DEAD:
                break;
        }
    }
    private void LookPlayer()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("BiteAttack")) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("FireBreathAttack")) return;
        Vector3 direction = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    private void PlayAttackPattern()
    {
        print("Start");
        int rand = Random.Range(0, 3);

        if (curState == State.ATTACK)
        {
            switch (rand)
            {
                case 0:
                    StartCoroutine(BiteAnimator());
                    break;
                case 1:
                    StartCoroutine(BreathAnimator());
                    break;
                case 2:
                    StartCoroutine(CastAnimator());
                    break;
            }
        }
        else 
        {
            StartCoroutine(StartPattern());
        }
    }
    private IEnumerator StartPattern()
    {
        yield return new WaitForSeconds(1.0f);
        PlayAttackPattern();
    }

    private IEnumerator Bite()
    {
        audioSource.clip = audioBite;
        audioSource.Play();
        StartCoroutine(CheckObject());
        yield return new WaitForSeconds(1.5f);
        
    }
    private IEnumerator CheckObject()
    {
        Debug.DrawRay(myCollider.transform.position + control, transform.forward * biteRange, Color.blue, 0.3f);
        if (Physics.Raycast(transform.position + control, transform.forward, out hitInfo, biteRange, layerMask))
        {
            playerControl.TakeDamage(5.0f);
            print(playerState.curHp);
        }
        yield return new WaitForSeconds(1.0f);
    }
    private IEnumerator BiteAnimator()
    {
        animator.SetTrigger("trigBite");
        yield return new WaitForSeconds(3.0f);
        PlayAttackPattern();
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
        yield return new WaitForSeconds(1.5f);
        
    }
    private IEnumerator BreathAnimator()
    {
        animator.SetTrigger("trigBreath");
        yield return new WaitForSeconds(3.0f);
        PlayAttackPattern();
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
        yield return new WaitForSeconds(4.0f);
        cast.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        Destroy(cast);
        yield return new WaitForSeconds(1.5f);

    }
    private IEnumerator CastAnimator()
    {
        animator.SetTrigger("trigCast");
        yield return new WaitForSeconds(3.0f);
        PlayAttackPattern();
    }

    public State GetState()
    {
        return curState;
    }
    private void DieAudio()
    {
        audioSource.clip = audioDie;
        audioSource.Play();
        MonsterUIManager.instance.SetActiveMonsterUI(false);
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
    private void Hurt(float value)
    {
        if (animator.GetBool("isDie")) return;
        animator.SetTrigger("trigHurt");
        curHp -= value;
        MonsterUIManager.instance.SetMonster(curHp, maxHp, "���� �巡��");
        MonsterUIManager.instance.SetActiveMonsterUI(true);
        animator.SetFloat("curHp", curHp);
        if (curHp <= 0)
        {
            animator.SetTrigger("trigDie");
            animator.SetBool("isDie", true);
            curState = State.DEAD;
        }
    }
}
