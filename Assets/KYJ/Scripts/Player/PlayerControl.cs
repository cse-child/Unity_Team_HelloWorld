using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
            animator.SetTrigger("Death");
        if (Input.GetKeyDown(KeyCode.F2))
            animator.SetTrigger("PickUp");
        // ���콺 ���� ��ư
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }
}
