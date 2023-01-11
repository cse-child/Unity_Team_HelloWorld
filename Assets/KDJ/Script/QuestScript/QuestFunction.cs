using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFunction : MonoBehaviour
{
    public NPCFunction npcFunction;
    public bool isKeyInput = false;

    private UIControl uiControl;

    private void Start()
    {
        uiControl = FindObjectOfType<UIControl>();
        npcFunction = GetComponent<NPCFunction>();
        foreach (Quest quest in QuestManager.instance.GetQuests())
        {
            quest.Start();
        }
    }

    private void Update()
    {
        Test();
        NPCGiveQuestToPlayer();
        QuestUpdate();
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
        if (GetQuestToNPC("qst_008").IsQuestClear())
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
            if (GetQuestToNPC("qst_001").IsQuestClear())
                QuestInteractionControl("qst_002");
            if (GetQuestToNPC("qst_002").IsQuestClear())
                QuestInteractionControl("qst_003");
            if (GetQuestToNPC("qst_003").IsQuestClear())
                QuestInteractionControl("qst_004");
            if (GetQuestToNPC("qst_004").IsQuestClear())
                QuestInteractionControl("qst_005");
        }
        else if(npcName.Contains("FortuneTeller"))
        {
            QuestInteractionControl("qst_007");
            if (GetQuestToNPC("qst_007").IsQuestClear())
                QuestInteractionControl("qst_008");
        }
    }

    private void AddQuestForPlayer(string questCode)
    {
        if (GetQuestToNPC(questCode).GetQuestActive())
        {
            foreach (Quest quest in QuestManager.instance.GetQuests())
            {
                if (questCode == quest.questInfo.questCode && quest.isClear)
                    PlayerClearQuest(questCode);
            }
            return;
        }
        if (GetQuestToNPC(questCode).IsQuestClear()) return;

        GetQuestToNPC(questCode).SetQuestActive(true);
        QuestDataManager.instance.AddQuest(questCode);

        uiControl.QuesteUI.transform.SetAsLastSibling();
        uiControl.QuesteUI.SetActive(true);
        uiControl.CheckCursorState();
        UISoundControl.instance.SoundPlay(1);
    }

    private void PlayerClearQuest(string questCode)
    {
        if (GetQuestToNPC(questCode) == null) return;
        if (!GetQuestToNPC(questCode).GetQuestActive())
        {
            QuestDataManager.instance.ClearQuest(questCode);
            return;
        }
        if (GetQuestToNPC(questCode).IsQuestClear())
        {
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
        {
            GetQuestToNPC("qst_006").SetQuestClear();
            isKeyInput = !isKeyInput;
        }
    }
}
