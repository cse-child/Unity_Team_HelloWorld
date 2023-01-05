using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCloseAttack : MonoBehaviour
{
    public bool isSword;
    public float range;
    public int damage;
    public float workSpeed;
    public float attackDelay; // 공격과 공격 사이의 딜레이 maybe 애니메이션간격으로할듯
    public float attackProcessing; // 공격판정되는 순간
    public float attackClose;// 모션 중 공격판정이 안되는 순간

    private Animator animator;
    private CapsuleCollider myCollider;
    private PlayerState playerState;
    private bool isAttack = false; // 공격중
    private bool isSwing = false; // 검 휘두르는 중

    private RaycastHit hitInfo; // 현재 무기에 닿은 오브젝트 정보
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
