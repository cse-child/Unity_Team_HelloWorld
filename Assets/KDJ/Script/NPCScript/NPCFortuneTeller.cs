using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFortuneTeller : MonoBehaviour
{
    public GameObject npcMovepoint;
    public Animator npcAnimator;
    public NPCFunction npcFunction;

    public float move = 0.0f;
    public bool isMove = false;

    private void Start()
    {
        npcMovepoint = GameObject.Find("NPC_MovePoint");
        npcFunction = GetComponent<NPCFunction>();
        npcAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckIsMove();
        npcAnimator.SetFloat("Move", move);
    }
    public void FortuneTellerMove()
    {
        if (isMove)
        {
            this.transform.LookAt(npcMovepoint.transform.position);
            this.transform.position =
                Vector3.MoveTowards(this.transform.position, npcMovepoint.transform.position, 4.5f * Time.deltaTime);
            if (this.transform.position.x == npcMovepoint.transform.position.x &&
                this.transform.position.z == npcMovepoint.transform.position.z)
            {
                npcFunction.SetIsFortuneTellerArrive(true);
                isMove = false;
            }
        }
        else
            return;
        
    }

    private void CheckIsMove()
    {
        if (isMove)
            move = 0.5f;
        else
            move = 0.0f;
    }

    public bool GetIsMove()
    {
        return isMove;
    }
    public void SetIsMove(bool input)
    {
        isMove = input;
    }
}
