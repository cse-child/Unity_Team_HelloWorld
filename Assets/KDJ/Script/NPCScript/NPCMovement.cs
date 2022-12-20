using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    private float npcMoveSpeed = 0.0f;
    private float npcRotSpeed = 0.5f;

    private float move = 0.0f;

    private Vector3 npcToPlayerDirection;

    private Animator npcAnimator;
    private PathFollower npcPathFollower;
    private NPCFunction npcFunction;
    private NPCReactionRange npcReactionRange;

    private bool isMove = false;
    private bool temp = false;

    private void Start()
    {
        npcAnimator = GetComponent<Animator>();
        npcPathFollower = GetComponent<PathFollower>();
        npcFunction = GetComponent<NPCFunction>();
        npcReactionRange = GetComponent<NPCReactionRange>();
        npcMoveSpeed = npcPathFollower.GetSpeed();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            temp = !temp;
            npcFunction.SetIsTalkingPlayerToNPC(temp);
        }

        CheckNPCBehavior();

        CheckIsMove();

        MoveForward();

        RotationToPlayer();

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
        {
            move = 0.5f;
            npcPathFollower.enabled = true;
        }
        else
        {
            move = 0.0f;
            npcPathFollower.enabled = false;
        }
            
    }
    public void SetIsMove(bool input)
    {
        isMove = input;
    }

    private void CheckNPCBehavior()
    {
        if (npcFunction.IsTalkingPlayerToNPC())
            isMove = false;
        else
            isMove = true;
    }

    private void RotationToPlayer()
    {
        if (!isMove)
        {
            npcToPlayerDirection = npcReactionRange.GetTargetDirection();
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation,
                Quaternion.LookRotation(npcToPlayerDirection), Time.deltaTime * npcRotSpeed);
        }
        else
            return;
    }
    public float GetNPCMoveSpeed()
    {
        return npcMoveSpeed;
    }

    public Vector3 GetNPCPosition()
    {
        return this.transform.position;
    }
}
