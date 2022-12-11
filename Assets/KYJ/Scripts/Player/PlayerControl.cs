using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotSpeed = 1.0f;

    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private float mouseX = 0.0f;

    private readonly int hashJump = Animator.StringToHash("Jump");

    private Vector3 direction;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");

        direction = Vector3.right * horizontal + Vector3.forward * vertical;

        if (direction.sqrMagnitude > 1.0f)
        {
            direction.Normalize();
        }

        transform.Translate(direction * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotSpeed * mouseX * Time.deltaTime);

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(hashJump);
        }
    }
}
