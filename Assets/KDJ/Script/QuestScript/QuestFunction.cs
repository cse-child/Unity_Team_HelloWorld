using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFunction : MonoBehaviour
{
    public NPCFunction npcFunction;
    public bool isKeyInput = false;

    public PlayerState playerState;
    private UIControl uiControl;
    public PlayerControl playerControl;

    private void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        playerState = FindObjectOfType<PlayerState>();
        uiControl = FindObjectOfType<UIControl>();
        npcFunction = GetComponent<NPCFunction>();
        foreach (Quest quest in QuestManager.instance.GetQuests())
        {
            quest.Start();
        }
    }

    private void Update()
    {
        //Test();
        QuestUpdate();
        NPCGiveQuestToPlayer();
    }

    private void FixedUpdate()
    {
        foreach (Quest quest in QuestManager.instance.GetQuests())
        {
            if (playerState.level >= quest.questInfo.questRequireLevel)
                quest.SetQuestAvailable(true);
        }
    }
    private void QuestUpdate()
    {
        foreach (Quest quest in QuestManager.instance.GetQuests())
        {
            quest.Update();
        }
    }

    private Quest GetQuestToNPC(string questCode)
    {
        foreach(Quest quest in QuestManager.instance.GetQuests())
        {
            if (questCode == quest.questInfo.questCode)
                return quest;
        }
        return null;
    }

    private void NPCGiveQuestToPlayer()
    {
        if (DefineQuestClear("qst_008"))
            QuestInteractionControl("qst_009");

        if (!npcFunction.IsTalkingPlayerToNPC()) return;

        string npcName = this.name;

        if (npcName.Contains("Blacksmith"))
        {
            QuestInteractionControl("qst_006");
        }
        else if (npcName.Contains("Bartender"))
        {
            QuestInteractionControl("qst_001");
            if (DefineQuestClear("qst_001"))
                QuestInteractionControl("qst_002");
            if (DefineQuestClear("qst_002"))
                QuestInteractionControl("qst_003");
            if (DefineQuestClear("qst_003"))
                QuestInteractionControl("qst_004");
            if (DefineQuestClear("qst_004"))
                QuestInteractionControl("qst_005");
        }
        else if(npcName.Contains("FortuneTeller"))
        {
            QuestInteractionControl("qst_007");
            if (DefineQuestClear("qst_007"))
                QuestInteractionControl("qst_008");
        }

        npcFunction.SetIsTalkingPlayerToNPC(false);
    }

    private void AddQuestForPlayer(string questCode)
    {
        uiControl.QuesteUI.transform.SetAsLastSibling();
        uiControl.QuesteUI.SetActive(true);
        uiControl.CheckCursorState();
        UISoundControl.instance.SoundPlay(1);
        if (GetQuestToNPC(questCode).IsQuestClear()) return;

        GetQuestToNPC(questCode).SetQuestPlayerControl(playerControl);
        GetQuestToNPC(questCode).SetQuestNPCFunction(npcFunction);
        GetQuestToNPC(questCode).SetQuestActive(true);
        QuestDataManager.instance.AddQuest(questCode);

    }

    private void PlayerClearQuest(string questCode)
    {
        if (GetQuestToNPC(questCode) == null) return;
        if (!GetQuestToNPC(questCode).GetQuestActive()) return;

        if (GetQuestToNPC(questCode).IsQuestGoalArrival() && npcFunction.IsTalkingPlayerToNPC())
        {
            npcFunction.SetPlayerClearQuestToNPC(true);
            if (GetQuestToNPC(questCode).IsQuestClear())
            {
                GetQuestToNPC(questCode).SetQuestActive(false);
            }
            QuestDataManager.instance.ClearQuest(questCode);
        }
    }

    private void QuestInteractionControl(string questCode)
    {
        if (GetQuestToNPC(questCode).GetQuestActive())
        {
            foreach (Quest quest in QuestManager.instance.GetQuests())
            {
                if (quest.questInfo.questCode == questCode)
                {
                    PlayerClearQuest(questCode);
                    break;
                }
            }
        }
        if (!GetQuestToNPC(questCode).GetQuestActive() && GetQuestToNPC(questCode).GetQuestAvailable())
            AddQuestForPlayer(questCode);
    }

    private bool DefineQuestClear(string questCode)
    {
        bool temp;

        if (GetQuestToNPC(questCode).IsQuestClear() && !GetQuestToNPC(questCode).GetQuestActive())
            temp = true;
        else
            temp = false;

        return temp;
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetQuestToNPC("qst_006").SetQuestClear();
            isKeyInput = !isKeyInput;
        }
    }
}
