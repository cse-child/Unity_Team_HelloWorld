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
    public float attackDelay; // ���ݰ� ���� ������ ������ maybe �ִϸ��̼ǰ��������ҵ�
    public float attackProcessing; // ���������Ǵ� ����
    public float attackClose;// ��� �� ���������� �ȵǴ� ����

    private Animator animator;
    private SphereCollider myCollider;
    private PlayerState playerState;
    private PlayerControl playerControl;
    private bool isAttack = false; // ������

    private RaycastHit hitInfo; // ���� ���⿡ ���� ������Ʈ ����
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
