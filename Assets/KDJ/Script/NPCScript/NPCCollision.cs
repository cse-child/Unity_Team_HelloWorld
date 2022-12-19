using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCCollision : MonoBehaviour
{
    NPCMovement npcMovement;

    private void OnCollisionEnter(Collision collsion)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcMovement.GetComponent<NPCMovement>().SetIsMove(false);
        }
    }
}
