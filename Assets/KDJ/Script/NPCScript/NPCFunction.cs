using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCFunction : MonoBehaviour
{
    public NPCRotation npcRotation;
    public bool isTalkingPlayerToNPC = false;

    private void Awake()
    {
        npcRotation = GetComponent<NPCRotation>();
    }

    private void Update()
    {
        IsNPCRotation();
    }
    private void IsNPCRotation()
    {
        if (isTalkingPlayerToNPC)
            npcRotation.SetNPCRotation(true);
        else
            npcRotation.SetNPCRotation(false);
    }
    public void SetIsTalkingPlayerToNPC(bool input)
    {
        isTalkingPlayerToNPC = input;
    }

    public bool IsTalkingPlayerToNPC()
    {
        return isTalkingPlayerToNPC;
    }
}
