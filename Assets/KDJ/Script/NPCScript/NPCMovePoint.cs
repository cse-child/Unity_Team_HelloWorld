using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovePoint : MonoBehaviour
{
    public GameObject fortuneTeller;
    public NPCFortuneTeller npcFT;
    public NPCFunction npcFunction;
    public bool isCollision = false;

    void Start()
    {
        fortuneTeller = GameObject.Find("NPC_FortuneTeller");
        npcFT = fortuneTeller.GetComponent<NPCFortuneTeller>();
        npcFunction = fortuneTeller.GetComponent<NPCFunction>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            npcFT.SetIsMove(false);
            npcFunction.SetIsFortuneTellerArrive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            npcFT.SetIsMove(false);
            npcFunction.SetIsFortuneTellerArrive(true);
        }
    }
}
