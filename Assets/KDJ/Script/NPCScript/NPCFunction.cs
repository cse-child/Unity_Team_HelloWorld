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
    public bool isPlayerClearQuestToNPC = false;
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
            StartCoroutine(PlayerPosChainge());
        }
    }
    private void PlayerTalkingToNPC()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsPlayerAccessNPC())
            SetIsTalkingPlayerToNPC(true);
        else if(!IsPlayerAccessNPC() /*|| Input.GetKeyUp(KeyCode.F)*/)
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

    public void SetPlayerClearQuestToNPC(bool input)
    {
        isPlayerClearQuestToNPC = input;
    }

    public bool IsPlayerClearQuestToNPC()
    {
        return isPlayerClearQuestToNPC;
    }
    IEnumerator PlayerPosChainge()
    {
        yield return new WaitForSeconds(0.2f);

        StarterAssets.ThirdPersonController thirdPerson = FindObjectOfType<StarterAssets.ThirdPersonController>();
        thirdPerson.TP(new Vector3(7, 0, 5));
    }
}
