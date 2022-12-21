using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    const int MAX_WEAPON_COUNT = 1;

    public GameObject weaponSocket;
    public GameObject attackCollision;

    private Animator animator;

    private PlayerState playerState;

    private int curWeaponState;

    private readonly int hashDeath = Animator.StringToHash("Death");
    private readonly int hashLooting = Animator.StringToHash("Looting");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashDamage = Animator.StringToHash("Damage");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
    }

    private void Start()
    {
        if(animator.GetInteger("WeaponState") == 0)
        {
            if(weaponSocket)
                weaponSocket.SetActive(false);
        }

    }

    private void Update()
    {
        Looting();
        Attack();
        EquippedWeapon();
        Skill();
    }

    // Player ������ -> HP ����
    public void TakeDamage(float value)
    {
        animator.SetTrigger(hashDamage);
        playerState.DecreaseHp(value);

        // �׾���?
        if(playerState.curHp <= 0)
        {
            animator.SetTrigger(hashDeath);
        }
    }

    // ���콺 ���� ��ư - ����
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(hashAttack);
        }
    }

    // ������ �ݱ�
    private void Looting()
    {
        if(Input.GetKeyDown(KeyCode.Z))
            animator.SetTrigger(hashLooting);
    }

    // ���� ����/����
    private void EquippedWeapon()
    {
        curWeaponState = animator.GetInteger("WeaponState");

        // ���� ����/����/����
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (curWeaponState == MAX_WEAPON_COUNT)
                animator.SetInteger("WeaponState", 0); // �Ǽ�
            else
                animator.SetInteger("WeaponState", ++curWeaponState); // ��������

            // ���⸦ �ٲٸ� ������ �Էµ� ����(Trigger, Integer) �ʱ�ȭ
            animator.ResetTrigger(hashAttack);
            animator.ResetTrigger(hashDamage);
            ResetSkillState();
        }
    }

    // �ִϸ��̼� Event - ���� Prefab Active On/Off
    private void ChangeWeaponActive()
    {
        if (weaponSocket)
        {
            if(weaponSocket.activeSelf)
                weaponSocket.SetActive(false);
            else
                weaponSocket.SetActive(true);

        }
    }

    // ��ų ��� - 1,2,3,4 ��
    private void Skill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            animator.SetInteger("SkillState", 1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            animator.SetInteger("SkillState", 2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            animator.SetInteger("SkillState", 3);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            animator.SetInteger("SkillState", 4);
    }

    // �ִϸ��̼� Event - ��ų ���� �ʱ�ȭ
    private void ResetSkillState()
    {
        animator.SetInteger("SkillState", 0);
    }

    // �ִϸ��̼� Event - ��հ� ���� Collider Ȱ��ȭ
    private void OnAttackCollision()
    {
        //print("OnAttackCollision");
        attackCollision.SetActive(true);
    }

    // �ִϸ��̼� Event - Skill Effect Play �Լ�
    private void PlaySkillEffect(string key)
    {
        float offsetZ = 7;

        ParticleManager.instance.Play(key, transform.position + transform.forward * offsetZ, transform.rotation);
    }
}
