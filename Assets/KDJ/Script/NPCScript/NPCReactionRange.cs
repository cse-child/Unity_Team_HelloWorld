using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCReactionRange : MonoBehaviour
{
    private NPCMovement npcMovement;
    Vector3 direction;

    private void Awake()
    {
        npcMovement = GetComponent<NPCMovement>();
    }
    private void OnCollisionEnter(Collision collsion)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcMovement.SetIsMove(false);
            direction = other.transform.position - npcMovement.GetNPCPosition();
        }
        else
        {
            npcMovement.SetIsMove(true);
        }
    }

    public Vector3 GetTargetDirection()
    {
        return direction;
    }
}
