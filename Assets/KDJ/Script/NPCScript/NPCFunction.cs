using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NPCFunction : MonoBehaviour
{
    public NPCRotation npcRotation;
    public GameObject player;
   
    public bool isPlayerAccessNPC = false;
    public bool isTalkingPlayerToNPC = false;
    public bool isTeleport = false;
    private void Awake()
    {
        npcRotation = GetComponent<NPCRotation>();
        player = GameObject.FindWithTag("Player");
        this.AddComponent<QuestFunction>();
    }

    private void Update()
    {
        IsNPCRotation();
        PlayerTalkingToNPC();
        PlayerTeleport();
    }
    private void IsNPCRotation()
    {
        if (isPlayerAccessNPC)
            npcRotation.SetNPCRotation(true);
        else
            npcRotation.SetNPCRotation(false);
    }
    private void PlayerTeleport()
    {
        if (IsTalkingPlayerToNPC() && this.name.Contains("Priest") && !GetIsTeleport())
        {
            player.transform.position = new Vector3(7, 0, 5);
            isTeleport = true;
        }
    }
    private void PlayerTalkingToNPC()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsPlayerAccessNPC())
            SetIsTalkingPlayerToNPC(true);
        else if(!IsPlayerAccessNPC())
            SetIsTalkingPlayerToNPC(false);
    }

    private bool GetIsTeleport()
    {
        return isTeleport;
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
