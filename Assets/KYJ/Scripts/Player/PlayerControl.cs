using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator animator;

    const int MAX_WEAPON_COUNT = 1;

    public GameObject weaponSocket;

    private int curWeaponState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        curWeaponState = animator.GetInteger("WeaponState");

        if (Input.GetKeyDown(KeyCode.F1))
            animator.SetTrigger("Death");
        if (Input.GetKeyDown(KeyCode.F2))
            animator.SetTrigger("Looting");
        // 마우스 왼쪽 버튼
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

        // 무기 장착/해제/변경
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (curWeaponState == MAX_WEAPON_COUNT)
            {
                animator.SetInteger("WeaponState", 0); // 맨손
                if(weaponSocket)
                    weaponSocket.SetActive(false);
            }
            else
            {
                animator.SetInteger("WeaponState", ++curWeaponState); // 무기장착
                if (weaponSocket)
                    weaponSocket.SetActive(true);
            }
        }
    }
}
