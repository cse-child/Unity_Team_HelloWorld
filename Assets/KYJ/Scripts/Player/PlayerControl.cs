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
        // ���콺 ���� ��ư
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

        // ���� ����/����/����
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (curWeaponState == MAX_WEAPON_COUNT)
            {
                animator.SetInteger("WeaponState", 0); // �Ǽ�
                if(weaponSocket)
                    weaponSocket.SetActive(false);
            }
            else
            {
                animator.SetInteger("WeaponState", ++curWeaponState); // ��������
                if (weaponSocket)
                    weaponSocket.SetActive(true);
            }
        }
    }
}
