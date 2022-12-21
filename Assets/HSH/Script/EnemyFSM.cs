using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {  None = -1, Idle = 0, Wander, Pursuit,}
public class EnemyFSM : MonoBehaviour
{
    [Header("Pursuit")]
    [SerializeField]
    private float targetRecognitionRange = 8; //�νĹ��� ������ prusuit ���·� ����
    [SerializeField]
    private float pursuitLimitRange = 10; // ���� ���� �̹��� �ٱ����� ������ wander ���·� ����

    private EnemyState enemyState = EnemyState.None; //���� �� �ൿ

    private Status status; //�̵��ӵ� ���� ����
    private NavMeshAgent navMeshAgent; // �̵� ��� ���� NavMeshAgent
    private GameObject target; // �÷��̾�
    private Animator animator;

    private void Awake()
    {
        status = GetComponent<Status>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.updateRotation = false; // NavMeshAgent ������Ʈ���� ȸ�� ������Ʈ x

        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        ChangeState(EnemyState.Idle);
    }
    private void OnEnable()
    {
        //���� Ȱ��ȭ�� �� ���� ���¸� "���"�� ����
        ChangeState(EnemyState.Idle);
    }
    private void OnDisable()
    {
        // ���� ��Ȱ��ȭ�� �� ���� ������� ���¸� �����ϰ� ���¸� none���� ����
        StopCoroutine(enemyState.ToString());

        enemyState = EnemyState.None;
    }

    public void ChangeState(EnemyState newState)
    {
        if (enemyState == newState) return; //���� ������� ���¿� �ٲٷ��� �ϴ� ���°� ������ �ٲ��� ����

        StopCoroutine(enemyState.ToString()); // ���� ������� ���� ����
        enemyState = newState; // ������¸� newState�� ����
        StartCoroutine(enemyState.ToString()); // ���ο� ���� ���
    }

    private IEnumerable Idle()
    {
        StartCoroutine("AutoChangeFromIdleToWander");

        while(true)
        {
            //Ÿ�ٰ��� �Ÿ��� ���� �ൿ ���� (��ȸ, �߰� ���Ÿ� ����)
            CalculateDistanceToTargetAndSelectState();
            //��� �����϶�
            yield return null;  
        }
    }
    private IEnumerable AutoChangeFromIdleToWander()
    {
        int changeTime = Random.Range(1, 5); //1~4�� ���

        yield return new WaitForSeconds(changeTime);

        ChangeState(EnemyState.Wander); //���¸� ��ȸ�� ����
    }
    private IEnumerable Wander()
    {
        float currentTime = 0;
        float maxTime = 10;

        // �̵��ӵ� ����
        navMeshAgent.speed = status.WalkSpeed;

        //��ǥ ��ġ ����
        navMeshAgent.SetDestination(CalculateWanderPosition());

        //��ǥ ��ġ�� ȸ��
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
                //���¸� "���"�� ����
                ChangeState(EnemyState.Idle);
            }

            CalculateDistanceToTargetAndSelectState();

            yield return null; 
        }
    }

    private Vector3 CalculateWanderPosition()
    {
        float wanderRadius = 10.0f; // ���� ��ġ�� �������� �ϴ� ���� ������
        int wanderJitter = 0; // ���õ� ���� (wanderJitterMin ~ wanderJitterMax)
        int wanderJitterMin = 0; // �ּ� ����
        int wanderJitterMax = 360; // �ִ� ����

        //���� �� ĳ���Ͱ� �ִ� ������ �߽� ��ġ�� ũ�� (������ ��� �ൿ x)
        Vector3 rangePosition = Vector3.zero;
        Vector3 rangeScale = Vector3.one * 100.0f;

        //�ڽ��� ��ġ�� �߽����� ������(wanderRadius) �Ÿ�, ���õ� ����(wanderJitter)�� ��ġ�� ��ǥ�� ��ǥ�������� ����
        wanderJitter = Random.Range(wanderJitterMin, wanderJitterMax);
        Vector3 targetPosition = transform.position + SetAngle(wanderRadius, wanderJitter);

        // ���� ��ǥ��ġ�� �ڽ��� �̵������� ����� �ʰ� ����
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
            //�̵��ӵ� ���� (��ȸ�� ���� �ȴ� �ӵ�, ������ �ٴ� �ӵ�)
            navMeshAgent.speed = status.RunSpeed;

            //��ǥ��ġ�� ���� �÷��̾��� ��ġ�� ����
            navMeshAgent.SetDestination(target.transform.position);

            //Ÿ�� ������ ��� �ֽ�
            LookRotationToTarget();

            //Ÿ�ٰ��� �Ÿ��� ���� �ൿ ����(��ȸ, �߰�, ���Ÿ� ����)
            CalculateDistanceToTargetAndSelectState();
        }
    }

    private void LookRotationToTarget()
    {
        //��ǥ ��ġ
        Vector3 to = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        //�÷��̾� ��ġ
        Vector3 from = new Vector3(target.transform.position.x, 0, target.transform.position.z);

        //�ٷ� ����
        transform.rotation = Quaternion.LookRotation(to - from);
        //������ ����
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
        //��ȸ���� �̵��� ��� ǥ��
        Gizmos.color = Color.black;
       // Gizmos.DrawRay(transform.position, navMeshAgent.destination - transform.position);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetRecognitionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pursuitLimitRange);

    }
}

