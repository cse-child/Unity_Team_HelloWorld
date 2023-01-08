using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NPCFunction : MonoBehaviour
{
    public NPCRotation npcRotation;
    public bool isPlayerAccessNPC = false;
    public bool isTalkingPlayerToNPC = false;

    private void Awake()
    {
        npcRotation = GetComponent<NPCRotation>();
        this.AddComponent<QuestFunction>();
    }

    private void Update()
    {
        IsNPCRotation();
    }
    private void IsNPCRotation()
    {
        if (isPlayerAccessNPC)
            npcRotation.SetNPCRotation(true);
        else
            npcRotation.SetNPCRotation(false);
    }

    public void SetIsPlayerAccessNPC(bool input)
    {
        isPlayerAccessNPC = input;
    }

    public bool IsPlayerAccessNPC()
    {
        return isPlayerAccessNPC;
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
