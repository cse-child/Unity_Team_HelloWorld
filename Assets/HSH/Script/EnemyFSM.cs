using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {  None = -1, Idle = 0, Wander, Pursuit,}
public class EnemyFSM : MonoBehaviour
{
    [Header("Pursuit")]
    [SerializeField]
    private float targetRecognitionRange = 8; //인식범위 들어오면 prusuit 상태로 변경
    [SerializeField]
    private float pursuitLimitRange = 10; // 추적 범위 이범위 바깥으로 나가면 wander 상태로 변경

    private EnemyState enemyState = EnemyState.None; //현재 적 행동

    private Status status; //이동속도 등의 정보
    private NavMeshAgent navMeshAgent; // 이동 제어를 위한 NavMeshAgent
    private GameObject target; // 플레이어
    private Animator animator;

    private void Awake()
    {
        status = GetComponent<Status>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.updateRotation = false; // NavMeshAgent 컴포넌트에서 회전 업데이트 x

        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        ChangeState(EnemyState.Idle);
    }
    private void OnEnable()
    {
        //적이 활성화될 때 적의 상태를 "대기"로 설정
        ChangeState(EnemyState.Idle);
    }
    private void OnDisable()
    {
        // 적이 비활성화될 때 현재 재생중인 상태를 종료하고 상태를 none으로 설정
        StopCoroutine(enemyState.ToString());

        enemyState = EnemyState.None;
    }

    public void ChangeState(EnemyState newState)
    {
        if (enemyState == newState) return; //현재 재생중인 상태와 바꾸려고 하는 상태가 같으면 바꾸지 않음

        StopCoroutine(enemyState.ToString()); // 이전 재생중인 상태 종료
        enemyState = newState; // 현재상태를 newState로 변경
        StartCoroutine(enemyState.ToString()); // 새로운 상태 재생
    }

    private IEnumerable Idle()
    {
        StartCoroutine("AutoChangeFromIdleToWander");

        while(true)
        {
            //타겟과의 거리의 따라 행동 선택 (배회, 추격 원거리 공격)
            CalculateDistanceToTargetAndSelectState();
            //대기 상태일때
            yield return null;  
        }
    }
    private IEnumerable AutoChangeFromIdleToWander()
    {
        int changeTime = Random.Range(1, 5); //1~4초 대기

        yield return new WaitForSeconds(changeTime);

        ChangeState(EnemyState.Wander); //상태를 배회로 변경
    }
    private IEnumerable Wander()
    {
        float currentTime = 0;
        float maxTime = 10;

        // 이동속도 설정
        navMeshAgent.speed = status.WalkSpeed;

        //목표 위치 설정
        navMeshAgent.SetDestination(CalculateWanderPosition());

        //목표 위치로 회전
        Vector3 to = new Vector3(navMeshAgent.destination.x, 0 , navMeshAgent.destination.z);
        Vector3 from = new Vector3(transform.position.x, 0 , transform.position.z);
        transform.rotation = Quaternion.LookRotation(to - from);

        while (true)
        {
            currentTime += Time.deltaTime;

            to = new Vector3(navMeshAgent.destination.x, 0, navMeshAgent.destination.z);
            from = new Vector3(transform.position.x, 0, transform.position.z);
            if ((to - from).sqrMagnitude < 0.01f || currentTime >= maxTime)
            {
                //상태를 "대기"로 변경
                ChangeState(EnemyState.Idle);
            }

            CalculateDistanceToTargetAndSelectState();

            yield return null; 
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

        // 생성 목표위치가 자신의 이동구역을 벗어나지 않게 조절
        targetPosition.x = Mathf.Clamp(targetPosition.x, rangePosition.x - rangeScale.x * 0.5f, rangePosition.x * 0.5f);
        targetPosition.y = 0.0f;
        targetPosition.z = Mathf.Clamp(targetPosition.z, rangePosition.z - rangeScale.z * 0.5f, rangePosition.z * 0.5f);

        return targetPosition;
    }

    private Vector3 SetAngle(float radius, int angle)
    {
        Vector3 position = Vector3.zero;

        position.x = Mathf.Cos(angle) * radius;
        position.z = Mathf.Sin(angle) * radius;
        return position;
    }

    private IEnumerable Pursuit()
    {
        while (true)
        {
            //이동속도 설정 (배회할 때는 걷는 속도, 추적은 뛰는 속도)
            navMeshAgent.speed = status.RunSpeed;

            //목표위치를 현재 플레이어의 위치로 설정
            navMeshAgent.SetDestination(target.transform.position);

            //타겟 방향을 계속 주시
            LookRotationToTarget();

            //타겟과의 거리에 따라 행동 선택(배회, 추격, 원거리 공격)
            CalculateDistanceToTargetAndSelectState();
        }
    }

    private void LookRotationToTarget()
    {
        //목표 위치
        Vector3 to = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        //플레이어 위치
        Vector3 from = new Vector3(target.transform.position.x, 0, target.transform.position.z);

        //바로 돌기
        transform.rotation = Quaternion.LookRotation(to - from);
        //서서히 돌기
        //Quaternion rotation = Quaternion.LookRotation(to - from);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.01f);
    }
    private void CalculateDistanceToTargetAndSelectState()
    {
        if (target == null) return;

        float distance = Vector3.Distance(target.transform.position, transform.position);

        if(distance <= targetRecognitionRange)
        {
            ChangeState(EnemyState.Pursuit);
        }
        else if ( distance >= pursuitLimitRange)
        {
            ChangeState(EnemyState.Wander);
        }
    }

    private void OnDrawGizmos()
    {
        //배회상태 이동할 경로 표시
        Gizmos.color = Color.black;
       // Gizmos.DrawRay(transform.position, navMeshAgent.destination - transform.position);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetRecognitionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pursuitLimitRange);

    }
}

