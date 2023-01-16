using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NPCFunction : MonoBehaviour
{
    public NPCRotation npcRotation;
    public Animator npcAnimator;
    public NPCMovement npcMovement;
    public GameObject player;
    public GameObject npcMovepoint;

    private NPCFortuneTeller fortuneTeller;
    public AudioSource audioSource;
   
    public bool isPlayerAccessNPC = false;
    public bool isTalkingPlayerToNPC = false;
    public bool isPlayerClearQuestToNPC = false;
    public bool isTeleport = false;
    public bool isFortuneTellerArrivePoint = false;
    private void Awake()
    {
        npcAnimator = GetComponent<Animator>();
        npcRotation = GetComponent<NPCRotation>();
        player = GameObject.FindWithTag("Player");
        npcMovepoint = GameObject.Find("NPC_MovePoint");
        this.AddComponent<QuestFunction>();
        if (this.name.Contains("FortuneTeller"))
        {
            this.AddComponent<NPCFortuneTeller>();
            fortuneTeller = GetComponent<NPCFortuneTeller>();
        }
        if(this.name.Contains("Blacksmith"))
        {
            audioSource = GetComponent<AudioSource>();
        }
            

    }

    private void Update()
    {
        if (this.name.Contains("FortuneTeller"))
        {
            if(!fortuneTeller.GetIsMove())
            {
                IsNPCRotation();
                PlayerTalkingToNPC();
            }
            FortuneTellerGoDungeon();
        }
        IsNPCRotation();
        PlayerTalkingToNPC();
        if (this.name.Contains("BlackSmith"))
            NPCSoundPlay(audioSource);
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

    private void FortuneTellerGoDungeon()
    {
        if (isFortuneTellerArrivePoint)
        {
            fortuneTeller.SetIsMove(false);
            return;
        }
        else if (this.name.Contains("FortuneTeller") && QuestManager.instance.GetQuest("qst_007").isActive)
        {
            fortuneTeller.SetIsMove(true);
            fortuneTeller.FortuneTellerMove();
        }
    }
    private void PlayerTalkingToNPC()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsPlayerAccessNPC())
        {
            SetIsTalkingPlayerToNPC(true);

        }
        else if(!IsPlayerAccessNPC() /*|| Input.GetKeyUp(KeyCode.F)*/)
            SetIsTalkingPlayerToNPC(false);
    }

    private void NPCSoundPlay(AudioSource source)
    {
        if (isTalkingPlayerToNPC)
            source.Play();
        else
            source.Stop();
        source.loop = false;
        source.time = 0;
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

    public bool IsFortuneTellerArrive()
    {
        return isFortuneTellerArrivePoint;
    }
    public void SetIsFortuneTellerArrive(bool input)
    {
        isFortuneTellerArrivePoint = input;
    }
    public Animator GetNPCAnimator()
    {
        return npcAnimator;
    }
    public NPCFortuneTeller GetNPCFortuneTeller() 
    {
        return fortuneTeller;
    }
    IEnumerator PlayerPosChainge()
    {
        yield return new WaitForSeconds(0.2f);

        StarterAssets.ThirdPersonController thirdPerson = FindObjectOfType<StarterAssets.ThirdPersonController>();
        thirdPerson.TP(new Vector3(7, 0, 5));
    }
}
