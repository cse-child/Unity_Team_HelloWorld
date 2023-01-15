using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncectCloseAttack : MonoBehaviour
{
    public bool isSword;
    public float range;
    public int damage;
    public float workSpeed;
    public float attackDelay; // ���ݰ� ���� ������ ������ maybe �ִϸ��̼ǰ��������ҵ�
    public float attackProcessing; // ���������Ǵ� ����
    public float attackClose;// ��� �� ���������� �ȵǴ� ����

    private Animator animator;
    private SphereCollider myCollider;
    private PlayerState playerState;
    private PlayerControl playerControl;
    private bool isAttack = false; // ������
    private bool isSwing = false; // �� �ֵθ��� ��

    private RaycastHit hitInfo; // ���� ���⿡ ���� ������Ʈ ����
    public LayerMask layerMask;
    Vector3 control = new Vector3(0, 0, 0);
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Hit");
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        myCollider = GetComponent<SphereCollider>();
        playerState = FindObjectOfType<PlayerState>();
        playerControl = FindObjectOfType<PlayerControl>();
    }
    private void Update()
    {

    }
    public void TryAttack()
    {
        animator.SetBool("isAttack", true);
        if (!isAttack)
        {
            //StartCoroutine(AttackCoroutine());
            animator.SetTrigger("trigAttack");
            isAttack = true;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttack = true;

        yield return new WaitForSeconds(attackProcessing);
        isSwing = true;
        StartCoroutine(CheckObject());

        //yield return new WaitForSeconds(attackClose);
        isSwing = false;

        //yield return new WaitForSeconds(attackDelay - attackProcessing - attackClose);
        isAttack = false;
    }

    private IEnumerator CheckObject()
    {
        Debug.DrawRay(myCollider.transform.position + control, transform.forward * range, Color.blue, 0.3f);
        if (Physics.Raycast(transform.position + control, transform.forward, out hitInfo, range, layerMask))
        {
            animator.SetTrigger("trigAttack");
            playerControl.TakeDamage(CalDamage());
            print(playerState.curHp);
            Debug.Log(hitInfo.transform.name);
            isSwing = false;
        }
        yield return new WaitForSeconds(1.0f);
    }
    private void StartCheck()
    {
        StartCoroutine(CheckObject());
        isAttack = false;
    }


    private float CalDamage()
    {
        float rand = Random.Range(1, 5);
        float realDamage;
        if (CriticalAttack())
        {
            realDamage = (damage / playerState.curDef) * rand * 10.0f * 1.5f;
            print("Critical");
        }
        else
        {
            realDamage = (damage / playerState.curDef) * rand * 10.0f;
        }

        return realDamage;
    }

    private bool CriticalAttack()
    {
        bool isCritical = false;
        float rand = Random.Range(0, 10);

        switch (rand)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
                isCritical = false;
                break;
            case 9:
                isCritical = true;
                break;
        }
        return isCritical;
    }
}
