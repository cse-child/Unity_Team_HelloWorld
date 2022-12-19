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
        //SetAxes();
    }

    //private void SetAxes()
    //{
    //    horizontal = Input.GetAxis("Horizontal");
    //    vertical = Input.GetAxis("Vertical");
    //
    //    animator.SetFloat("Horizontal", horizontal);
    //    animator.SetFloat("Vertical", vertical);
    //
    //    print("Hori : " + horizontal + " / verti : " + vertical);
    //}

    private void PlayAnimations()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            animator.SetTrigger(hashDeath);
        if(Input.GetKeyDown(KeyCode.F2))
            animator.SetTrigger(hashDamage);
        
    }

    // 마우스 왼쪽 버튼 - 공격
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger(hashAttack);
    }

    // 아이템 줍기
    private void Looting()
    {
        if(Input.GetKeyDown(KeyCode.Z))
            animator.SetTrigger(hashLooting);
    }

    // 무기 장착/해제
    private void EquippedWeapon()
    {
        curWeaponState = animator.GetInteger("WeaponState");

        // 무기 장착/해제/변경
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (curWeaponState == MAX_WEAPON_COUNT)
            {
                animator.SetInteger("WeaponState", 0); // 맨손
            }
            else
            {
                animator.SetInteger("WeaponState", ++curWeaponState); // 무기장착
            }
        }
    }

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
}
