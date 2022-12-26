using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCReactionRange : MonoBehaviour
{
    public NPCMovement npcMovement;
    public NPCFunction npcFunction;
    Vector3 direction;

    private void Awake()
    {
        npcFunction = transform.parent.GetComponent<NPCFunction>();
        npcMovement = transform.parent.GetComponent<NPCMovement>();
    }
    private void OnCollisionEnter(Collision collsion)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //npcFunction.SetIsTalkingPlayerToNPC(true);
            npcMovement.SetIsMove(false);
            direction = other.transform.position - this.transform.position;
        }
        else if(other == null)
        {
            npcFunction.SetIsTalkingPlayerToNPC(false);
        }
        else
        {
            npcFunction.SetIsTalkingPlayerToNPC(false);
        }
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

}
