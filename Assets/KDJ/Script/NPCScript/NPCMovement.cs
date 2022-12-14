using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    private float npcMoveSpeed = 2.0f;
    private float npcRotSpeed = 1.0f;

    private float move = 0.0f;

    private Animator npcAnimator;

    private bool isMove = false;
    private bool temp = false;

    private void Awake()
    {
        npcAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    temp = !temp;
        //    GetComponent<NPCFunction>().SetIsTalkingPlayerToNPC(temp);
        //}

        CheckNPCBehavior();

        CheckIsMove();

        MoveForward();

        transform.Rotate(Vector3.up * npcRotSpeed * Time.deltaTime);

        npcAnimator.SetFloat("Move", move);
    }

    private void MoveForward()
    {
        if (!isMove) return;

        transform.Translate(Vector3.forward * npcMoveSpeed * Time.deltaTime);

    }

    private void CheckIsMove()
    {
        if (isMove)
            move = 1.0f;
        else
            move = 0.0f;
    }
    public void SetIsMove(bool input)
    {
        isMove = input;
    }

    private void CheckNPCBehavior()
    {
        if (GetComponent<NPCFunction>().IsTalkingPlayerToNPC())
            isMove = true;
        else
            isMove = false;
    }
}
