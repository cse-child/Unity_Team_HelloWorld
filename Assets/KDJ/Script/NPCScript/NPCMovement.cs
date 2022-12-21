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

    public Animator npcAnimator;
    public PathFollower npcPathFollower;
    public NPCFunction npcFunction;
    public NPCReactionRange npcReactionRange;

    private bool isMove;
    private bool temp = false;

    private void Start()
    {
        npcAnimator = GetComponent<Animator>();
        npcPathFollower = GetComponent<PathFollower>();
        npcFunction = GetComponent<NPCFunction>();
        npcReactionRange = transform.Find("NPC_ReactionRange").GetComponent<NPCReactionRange>();
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

        //CheckIsMove();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            npcPathFollower.SetIsStop(true);
        }

        MoveForward();

        npcAnimator.SetFloat("Move", move);
    }

    private void MoveForward()
    {
        if (!isMove) return;

        transform.Translate(Vector3.forward * npcMoveSpeed * Time.deltaTime);

    }

    void CheckIsMove()
    {
        if (GetIsMove())
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

    public float GetNPCMoveSpeed()
    {
        return npcMoveSpeed;
    }

    public Vector3 GetNPCPosition()
    {
        return this.transform.position;
    }

    private bool GetIsMove()
    {
        return isMove;
    }
}
