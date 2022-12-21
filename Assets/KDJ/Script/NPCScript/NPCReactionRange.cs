using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCReactionRange : MonoBehaviour
{
    public NPCMovement npcMovement;
    Vector3 direction;

    private void Awake()
    {
        npcMovement = transform.parent.GetComponent<NPCMovement>();
    }
    private void OnCollisionEnter(Collision collsion)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcMovement.SetIsMove(false);
        }
        else
        {
            npcMovement.SetIsMove(true);
        }
    }

}
