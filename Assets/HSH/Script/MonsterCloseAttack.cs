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
    private bool isAttack = false; // ������
    private bool isSwing = false; // �� �ֵθ��� ��

    private RaycastHit hitInfo; // ���� ���⿡ ���� ������Ʈ ����
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

    }
    public void TryAttack()
    {
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton@Attack01"))
        //{
        //    if(!isAttack)
        //    {
        //        StartCoroutine(AttackCoroutine());
        //    }
        //}
        //animator.SetBool("isAttack", true);
        //if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range))
        //{
        //    Debug.Log("hit point : " + hitInfo.point + ", distance : " + hitInfo.distance + ", name : " + hitInfo.collider.name);
        //    Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.red);
        //}
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

        yield return new WaitForSeconds(attackClose);
        isSwing = false;

        yield return new WaitForSeconds(attackDelay - attackProcessing - attackClose);
        isAttack = false;
    }

    private bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range))
        {
            return true;
        }
        return false;
    }
    private IEnumerator HitCoroutine()
    {
        while(isSwing)
        {
            if(CheckObject())
            {
                isSwing = false;
                print(isSwing);
                print(isAttack);
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }
}
