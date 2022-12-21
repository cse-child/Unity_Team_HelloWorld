using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState1 {  None = -1, Idle = 0, Wander, Pursuit,}
public class EnemyFSM1 : MonoBehaviour
{
    [Header("Pursuit")]
    [SerializeField]
    private float targetRecognitionRange = 8; //�νĹ��� ������ prusuit ���·� ����
    [SerializeField]
    private float pursuitLimitRange = 10; // ���� ���� �̹��� �ٱ����� ������ wander ���·� ����

    private EnemyState1 enemyState = EnemyState1.None; //���� �� �ൿ

    private Status status; //�̵��ӵ� ���� ����
    public GameObject target; // �÷��̾�
    private Animator animator;

    private void Awake()
    {
        status = GetComponent<Status>();
        animator = GetComponent<Animator>();

        //target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        ChangeState(EnemyState1.Idle);
    }
    private void Update()
    {
        if(Input.GetKey("z"))
        {
            transform.Translate(Vector3.forward * status.WalkSpeed * Time.deltaTime, Space.Self);
        }
    }
    private void OnEnable()
    {
        //���� Ȱ��ȭ�� �� ���� ���¸� "���"�� ����
        ChangeState(EnemyState1.Idle);
    }
    private void OnDisable()
    {
        // ���� ��Ȱ��ȭ�� �� ���� ������� ���¸� �����ϰ� ���¸� none���� ����
        StopCoroutine(enemyState.ToString());

        enemyState = EnemyState1.None;
    }

    public void ChangeState(EnemyState1 newState)
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

        ChangeState(EnemyState1.Wander); //���¸� ��ȸ�� ����
    }
    private IEnumerable Wander()
    {
        float currentTime = 0;
        float maxTime = 10;

        //��ǥ ��ġ�� ȸ��
        Vector3 to = new Vector3(CalculateWanderPosition().x, 0 , CalculateWanderPosition().z);
        Vector3 from = new Vector3(transform.position.x, 0 , transform.position.z);
        transform.rotation = Quaternion.LookRotation(to - from);

        transform.Translate(Vector3.forward * status.WalkSpeed * Time.deltaTime, Space.Self);

        while (true)
        {
            currentTime += Time.deltaTime;

            to = new Vector3(CalculateWanderPosition().x, 0, CalculateWanderPosition().z);
            from = new Vector3(transform.position.x, 0, transform.position.z);
            if ((to - from).sqrMagnitude < 0.01f || currentTime >= maxTime)
            {
                //���¸� "���"�� ����
                ChangeState(EnemyState1.Idle);
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
        transform.Translate(Vector3.forward * status.WalkSpeed * Time.deltaTime, Space.Self);
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
            ChangeState(EnemyState1.Pursuit);
        }
        else if ( distance >= pursuitLimitRange)
        {
            ChangeState(EnemyState1.Wander);
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

