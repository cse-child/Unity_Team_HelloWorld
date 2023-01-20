using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.IO;
using Guirao.UltimateTextDamage;

public class DragonAI : MonoBehaviour
{
    public UltimateTextDamageManager manager; //데미지 에셋
    public Transform trDamagePosition; // 데미지 에셋 출현 Pos
    private AudioSource audioSource; // 음성

    public Transform breathPort;// 브레스 출현 Pos
    public enum State // FSM
    {
        IDLE, TRACE, ATTACK, DEAD, HURT
    }
    private enum AttackPattern
    {
        BITE, BREATH, CAST, GUARD, FALLING
    }
    public State curState; //현재상태
    public State curAttackPattern;

    public float traceRange = 18.0f; //인식범위
    public float attackRange = 10.0f; // 공격 범위
    private float biteRange = 4.0f;
    public float moveSpeed = 2.0f; // 이동속도
    public float rotateSpeed = 0.5f; //방향전환 속도
    private int maxHp = 100; //최대체력
    private float curHp = 1000.0f; // 현재체력
    private float Exp = 1000.0f; // 사망 시 드랍 경험치

    private Vector3 randPos;

    private GameObject target; // 플레이어 태그
    private Animator animator; // 사용할 애니메이션
    public GameObject itemPrefab; // 사망 시 드랍 아이템 프리펩
    public GameObject CastPrefab; // 스킬 사용 시 생성되는 오브젝트
    public GameObject CannonPrefab; // 스킬 사용 시 생성되는 오브젝트 2

    public System.Action onDie; //사망시 사용 함수

    private readonly WaitForSeconds delayTime = new WaitForSeconds(1.0f); // 딜레이 타임

    private CapsuleCollider myCollider;
    private PlayerState playerState; //플레이어 상태 개입
    private PlayerControl playerControl; // 플레이어 피격구현 필요 정보
    private RaycastHit hitInfo; // 현재 무기에 닿은 오브젝트 정보
    public LayerMask layerMask; // 레이아웃 시 플레이어 레이어 구분
    Vector3 control = new Vector3(0, 0, 0);
    public AudioClip audioHurt;   // 사용 음성
    public AudioClip audioDie;    // 사용 음성
    public AudioClip audioBite;   // 사용 음성
    public AudioClip audioCast;   // 사용 음성
    public AudioClip audioBreath; // 사용 음성

    private void Awake() //할당을 할 때 한번만 실행되는 Awake에서
    {
        animator = GetComponent<Animator>(); // 애니메이션
        target = GameObject.FindGameObjectWithTag("Player"); //플레이어 타겟 지정
        playerState = FindObjectOfType<PlayerState>(); // 플레이어 공격시 hp접근용
        playerControl = FindObjectOfType<PlayerControl>(); // 피격 시 플레이어의 attack만큼 피가 줄어들 예정
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
        MonsterUIManager.instance.SetMonster(curHp, maxHp, "레드 드래곤");
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
