using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator animator;

    const int MAX_WEAPON_COUNT = 1;

    public GameObject weaponSocket;

    private int curWeaponState;

    //private float horizontal = 0.0f;
    //private float vertical = 0.0f;

    private readonly int hashDeath = Animator.StringToHash("Death");
    private readonly int hashLooting = Animator.StringToHash("Looting");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashDamage = Animator.StringToHash("Damage");

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        PlayAnimations();

        Looting();
        Attack();
        EquippedWeapon();
        Skill();
    }

    private void PlayAnimations()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            animator.SetTrigger(hashDeath);
        if(Input.GetKeyDown(KeyCode.F2))
            animator.SetTrigger(hashDamage);
        
    }

    // ���콺 ���� ��ư - ����
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger(hashAttack);
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

    // ���� Prefab Active On/Off
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

    private void ResetSkillState()
    {
        animator.SetInteger("SkillState", 0);
    }
}
