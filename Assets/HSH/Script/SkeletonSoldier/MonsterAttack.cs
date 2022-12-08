using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private Animator animator;

    private readonly int hashAttack = Animator.StringToHash("Attack");

    private bool isAttack = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isAttack && Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(hashAttack);
            isAttack = true;
        }
    }

    private void EndAttack()
    {
        isAttack = false;
    }
}
