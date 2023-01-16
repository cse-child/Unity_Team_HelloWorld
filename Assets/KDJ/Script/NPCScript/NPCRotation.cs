using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRotation : MonoBehaviour
{
    public bool isRotation = false;
    public NPCReactionRange npcReactionRange;

    private Quaternion tempDir;

    private void Awake()
    {
        npcReactionRange = transform.Find("NPC_ReactionRange").GetComponent<NPCReactionRange>();
        tempDir = this.transform.rotation;
    }

    void Update()
    {
        NPCLookAtPlayer();
        NPCLookForward();
    }

    private void NPCLookAtPlayer()
    {
        if (!isRotation) return;

        Vector3 dir = npcReactionRange.GetDirection();

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation,
            Quaternion.LookRotation(dir), Time.deltaTime * 2.0f);
    }

    private void NPCLookForward()
    {
        if (isRotation) return;

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation,
            tempDir, Time.deltaTime * 2.0f);
    }

    public void SetNPCRotation(bool input)
    {
        isRotation = input;
    }
}
