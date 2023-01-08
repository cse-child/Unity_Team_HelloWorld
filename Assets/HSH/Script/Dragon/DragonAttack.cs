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
    private bool isAttack = false; // ������

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
