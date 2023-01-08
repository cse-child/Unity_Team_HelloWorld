using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCReactionRange : MonoBehaviour
{
    public NPCFunction npcFunction;
    Vector3 direction;

    private void Awake()
    {
        npcFunction = transform.parent.GetComponent<NPCFunction>();
    }
    private void OnCollisionEnter(Collision collsion)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcFunction.SetIsPlayerAccessNPC(true);
            direction = other.transform.position - transform.parent.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            npcFunction.SetIsPlayerAccessNPC(false);
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

}
