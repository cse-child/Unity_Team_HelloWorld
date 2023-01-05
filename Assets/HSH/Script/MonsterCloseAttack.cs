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
    }
    private void Update()
    {

    }
    public void TryAttack()
    {
        if (!isAttack)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttack = true;
        animator.SetTrigger("trigAttack");

        yield return new WaitForSeconds(attackProcessing);
        isSwing = true;
        StartCoroutine(HitCoroutine());

        //yield return new WaitForSeconds(attackClose);
        isSwing = false;

        //yield return new WaitForSeconds(attackDelay - attackProcessing - attackClose);
        isAttack = false;
    }

    private bool CheckObject()
    {
        //if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range))
        //{
        //    return true;
        //}
        //return false;
        
        if (Physics.Raycast(transform.position + control, transform.forward, out hitInfo, range, layerMask))
        {
            //hitInfo.transform.GetComponent<PlayerHpTest>().Hurt();
            playerState.DecreaseHp(damage);
            print(playerState.curHp);
            return true;
        }
        return false;
    }
    private IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            Debug.DrawRay(myCollider.transform.position + control, transform.forward * range, Color.blue, 0.3f);
            if (CheckObject())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

}
