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
    private PlayerControl playerControl;
    private bool isAttack = false; // 공격중

    private RaycastHit hitInfo; // 현재 무기에 닿은 오브젝트 정보
    public LayerMask layerMask;
    Vector3 control = new Vector3(0, 0, 0);


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
        if (!isAttack)
        {
            animator.SetTrigger("trigAttack");
            isAttack = true;
        }
    }

    private IEnumerator CheckObject()
    {
        Debug.DrawRay(myCollider.transform.position + control, transform.forward * range, Color.blue, 0.3f);
        if (Physics.Raycast(transform.position + control, transform.forward, out hitInfo, range, layerMask))
        {
            playerControl.TakeDamage(5.0f);
            print(playerState.curHp);
        }
        yield return new WaitForSeconds(1.0f);
    }
    private void StartCheck()
    {
        StartCoroutine(CheckObject());
        isAttack = false;
    }

}
