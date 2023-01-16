using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float npcMoveSpeed = 0.0f;
    public float npcRotSpeed = 0.5f;

    public float move = 0.0f;

    public PathFollower npcPathFollower;
    public NPCFunction npcFunction;
    public Animator npcAnimator;
    public NPCReactionRange npcReactionRange;

    private bool isMove;

    private void Start()
    {
        npcPathFollower = GetComponent<PathFollower>();
        npcFunction = GetComponent<NPCFunction>();
        npcAnimator = npcFunction.GetNPCAnimator();
        npcReactionRange = transform.Find("NPC_ReactionRange").GetComponent<NPCReactionRange>();
        npcMoveSpeed = npcPathFollower.GetSpeed();
    }

    private void Update()
    {
        
        CheckNPCBehavior();

        CheckIsMove();


        npcAnimator.SetFloat("Move", move);
    }

    private void CheckIsMove()
    {
        if (GetIsMove())
        {
            move = 0.5f;
            npcPathFollower.SetIsStop(false);
        }
        else
        {
            move = 0.0f;
            npcPathFollower.SetIsStop(true);
        }
            
    }


    public void SetIsMove(bool input)
    {
        isMove = input;
    }

    private void CheckNPCBehavior()
    {
        if (npcFunction.IsPlayerAccessNPC())
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
