using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCloseAttack : MonoBehaviour
{
    public bool isSword;
    public float range;
    public int damage;
    public float workSpeed;
    public float attackDelay; // ���ݰ� ���� ������ ������ maybe �ִϸ��̼ǰ��������ҵ�
    public float attackProcessing; // ���������Ǵ� ����
    public float attackClose;// ��� �� ���������� �ȵǴ� ����

    private Animator animator;
    private CapsuleCollider myCollider;
    private PlayerState playerState;
    private PlayerControl playerControl;
    private bool isAttack = false; // ������
    private bool isSwing = false; // �� �ֵθ��� ��

    private RaycastHit hitInfo; // ���� ���⿡ ���� ������Ʈ ����
    public LayerMask layerMask;
    Vector3 control = new Vector3(0, 1, 0);
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
        myCollider = GetComponent<CapsuleCollider>();
        playerState = FindObjectOfType<PlayerState>();
        playerControl = FindObjectOfType<PlayerControl>();
    }
    private void Update()
    {

    }
    public void TryAttack()
    {
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
        //print("SS");
        Debug.DrawRay(myCollider.transform.position + control, transform.forward * range, Color.blue, 0.3f);
        if (Physics.Raycast(transform.position + control, transform.forward, out hitInfo, range, layerMask))
        {
            animator.SetTrigger("trigAttack");
            //playerState.DecreaseHp(damage);
            playerControl.TakeDamage(damage);
            //print(playerState.curHp);
            //Debug.Log(hitInfo.transform.name);
            isSwing = false;
        }
        yield return new WaitForSeconds(1.0f);
    }
    private void StartCheck()
    {
        StartCoroutine(CheckObject());
        isAttack = false;
    }


}
