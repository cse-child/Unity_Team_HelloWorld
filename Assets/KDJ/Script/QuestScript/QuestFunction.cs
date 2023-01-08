using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFunction : MonoBehaviour
{
    public NPCFunction npcFunction;

       
    private void Start()
    {
        npcFunction = GetComponent<NPCFunction>();
    }

    void Update()
    {
        Test();
        NPCGiveQuestToPlayer();
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
        if (Input.GetKeyDown(KeyCode.F) && npcFunction.IsPlayerAccessNPC())
            npcFunction.SetIsTalkingPlayerToNPC(true);
        else
            npcFunction.SetIsTalkingPlayerToNPC(false);

        if (!npcFunction.IsTalkingPlayerToNPC()) return;

        string npcName = this.name;

        if (npcName.Contains("Blacksmith"))
        {
            QuestInteractionControl("qst_006");
        }
        else if (npcName.Contains("Bartender"))
        {
            QuestInteractionControl("qst_001");
            if (GetQuestToNPC("qst_001").isClear)
                QuestInteractionControl("qst_002");
            if (GetQuestToNPC("qst_002").isClear)
                QuestInteractionControl("qst_003");
            if (GetQuestToNPC("qst_003").isClear)
                QuestInteractionControl("qst_004");
            if (GetQuestToNPC("qst_004").isClear)
                QuestInteractionControl("qst_005");
        }
        else if(npcName.Contains("FortuneTeller"))
        {
            QuestInteractionControl("qst_007");
            if (GetQuestToNPC("qst_007").isClear)
                QuestInteractionControl("qst_008");
        }

        if (GetQuestToNPC("qst_008").isClear)
            QuestInteractionControl("qst_009");
    }

    private void AddQuestForPlayer(string questCode)
    {
        if (GetQuestToNPC(questCode) != null) return;
        if (GetQuestToNPC(questCode).GetQuestActive()) return;
        if (GetQuestToNPC(questCode).isClear) return;

        QuestDataManager.instance.AddQuest(questCode);
        GetQuestToNPC(questCode).SetQuestActive(true);
    }

    private void PlayerClearQuest(string questCode)
    {
        if (GetQuestToNPC(questCode) == null) return;
        if (!GetQuestToNPC(questCode).GetQuestActive()) return;

        if (GetQuestToNPC(questCode).isClear)
        {
            QuestDataManager.instance.ClearQuest(questCode);
            GetQuestToNPC(questCode).SetQuestActive(false);
        }
    }

    private void QuestInteractionControl(string questCode)
    {
        AddQuestForPlayer(questCode);
        PlayerClearQuest(questCode);
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.B))
            GetQuestToNPC("qst_001").SetQuestClear();
        else
            return;
    }
}
