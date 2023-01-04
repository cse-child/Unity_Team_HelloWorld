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
        NPCGiveQuestToPlayer();
    }

    private void NPCGiveQuestToPlayer()
    {
        if (!npcFunction.IsTalkingPlayerToNPC()) return;

        string npcName = this.name;

        if (npcName.Contains("Blacksmith"))
        {
            QuestDataManager.instance.AddQuest("qst_006");
        }
        else if (npcName.Contains("Bartender"))
        {
            QuestDataManager.instance.AddQuest("qst_005");
        }
    }
}
