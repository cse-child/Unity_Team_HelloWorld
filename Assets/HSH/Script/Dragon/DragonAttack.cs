using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    enum Pattern
    {
        BITE, BREATH, CAST
    }
    public bool isSword;
    public float range;
    public int damage;
    public float workSpeed;
    public float attackDelay; // 공격과 공격 사이의 딜레이 maybe 애니메이션간격으로할듯
    public float attackProcessing; // 공격판정되는 순간
    public float attackClose;// 모션 중 공격판정이 안되는 순간

    private Animator animator;
    private SphereCollider myCollider;
    private PlayerState playerState;
    private bool isAttack = false; // 공격중

    private RaycastHit hitInfo; // 현재 무기에 닿은 오브젝트 정보
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
    }
    private void Update()
    {

    }
    public void TryAttack()
    {
        if (!isAttack)
        {
            isAttack = true;
            animator.SetTrigger("trigBite");
        }
    }

    private IEnumerator CheckObject()
    {
        print("SS");
        Debug.DrawRay(myCollider.transform.position + control, transform.forward * range, Color.blue, 0.3f);
        if (Physics.Raycast(transform.position + control, transform.forward, out hitInfo, range, layerMask))
        {
            DragonThink();
            
            Debug.Log(hitInfo.transform.name);
        }
        yield return new WaitForSeconds(1.0f);
    }
    private void StartCheck()
    {
        StartCoroutine(CheckObject());
        isAttack = false;
    }

    private void DragonThink()
    {
        int ranAction = Random.Range(0, 5);
        switch (ranAction)
        {
            case 0:
            case 1:
            case 2:
                Bite();
                break;
            case 3:
                Breath();
                break;
            case 4:
                Cast();
                break;
        }
    }
    private void Bite()
    {
        animator.SetTrigger("trigBite");
        playerState.DecreaseHp(damage);
        print(playerState.curHp);
    }
    private void Breath()
    {
        animator.SetTrigger("trigBreath");
        playerState.DecreaseHp(damage);
        print(playerState.curHp);
    }
    private void Cast()
    {
        animator.SetTrigger("trigCastSpell");
    }
}
