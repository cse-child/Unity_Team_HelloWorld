using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Quest
{
    
    public bool isActive = false;
    public bool isClear = false;

    public bool isQuestGoalArrival = false;

    private bool isQuestAvailable = false;

    public GameObject player;
    public GameObject enemy;

    private int countValue = 0;

    public struct QuestInfo
    {
        public string questCode;
        public string questType;
        public int questRequireLevel;
    }

    public QuestInfo questInfo;

    private string type;
    private int requireCount;
    private int itemNum = 0;
    private int healthPotionCount = 0;
    private int manaPotionCount = 0;

    private QuestAlarmManager.QuestDetail questDetail;

    private NPCFunction npcFunction;
    private PlayerControl playerControl;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthPotionCount = PlayerInventoryData.instance.GetItemCount(1);
        manaPotionCount = PlayerInventoryData.instance.GetItemCount(2);
        TypeDefinition();
    }

    public void Update()
    {
        if (!isActive) return;

        QuestApart();
        QuestClear();
    }

    private void TypeDefinition()
    {
        if (!questInfo.questType.Contains("_"))
            type = questInfo.questType;
        else
        {
            string[] temp = questInfo.questType.Split("_");
            type = temp[0];
            requireCount = int.Parse(temp[1]);
        }
    }

    private void QuestApart()
    {
        switch (type)
        {
            case "Tutorial":
                Tutorial();
                break;
            case "Hunting":
                Hunting();
                break;
            case "Collecting":
                Collecting();
                break;
            case "Researching":
                Researching();
                break;
        }
        if (Input.GetKeyDown(KeyCode.K))
            countValue++;
       QuestAlarmManager.instance.GetQuestAlarmData(questDetail.questName).goalData = countValue.ToString() + " / " + requireCount.ToString();
    }

    private void Tutorial()
    {
        if(this.questInfo.questCode == "qst_001")
        {
            if (npcFunction.IsTalkingPlayerToNPC())
            {
                isQuestGoalArrival = true;
            }
        }
        else if(this.questInfo.questCode == "qst_002")
        {
            if (!playerControl.IsPlayerStop())
            {
                isQuestGoalArrival = true;
            }
        }
        else if(this.questInfo.questCode == "qst_003")
        {
            if(playerControl.IsPlayerAttacking())
            {
                isQuestGoalArrival = true;
            }
        }
        else if(this.questInfo.questCode == "qst_004")
        {
            int tempNum1 = PlayerInventoryData.instance.GetItemCount(1);
            int tempNum2 = PlayerInventoryData.instance.GetItemCount(2);
            if (healthPotionCount > tempNum1 ||
                manaPotionCount > tempNum2)
                isQuestGoalArrival = true;
        }

        if (isQuestGoalArrival)
            QuestAlarmManager.instance.GetQuestAlarmData(questDetail.questName).questDetail.isSucceed = true;
    }

    private void Collecting()
    {
        if (this.questInfo.questCode == "qst_006")
        {
            itemNum = 24; //퀘스트 아이템 번호
            countValue = PlayerInventoryData.instance.GetItemCount(itemNum);
        }
        if (countValue >= requireCount)
        {
            isQuestGoalArrival = true;
            QuestAlarmManager.instance.GetQuestAlarmData(questDetail.questName).questDetail.isSucceed = true;
        }

    }

    private void Researching()
    {
        if (this.questInfo.questCode == "qst_007")
        {
            if (npcFunction.IsFortuneTellerArrive() && npcFunction.IsTalkingPlayerToNPC() && npcFunction.name.Contains("FortuneTeller"))
                isQuestGoalArrival = true;
        }
    }

    private void Hunting()
    {
        if(this.questInfo.questCode == "qst_005")
        {
            if (QuestCountManager.instance.GetSwitchOn()) return;
            countValue = QuestCountManager.instance.GetBearsDeathCount();
        }

        if(this.questInfo.questCode == "qst_008")
        {
            //QuestCountManager.instance.ScanSkeletonsDeathCount();
            //QuestCountManager.instance.ScanInsectsDeathCount();
            //countValue = QuestCountManager.instance.GetSkeletonsDeathCount() +
            //    QuestCountManager.instance.GetInsectsDeathCount();
        }

        if(this.questInfo.questCode == "qst_009")
        {
            countValue = 0;
        }

        if (countValue > requireCount)
            countValue = requireCount;

        if (countValue >= requireCount)
        {
            isQuestGoalArrival = true;
            QuestAlarmManager.instance.GetQuestAlarmData(questDetail.questName).questDetail.isSucceed = true;

            if(this.questInfo.questCode == "qst_005")
            {
                //QuestCountManager.instance.ResetBearDeathCount();
            }

            if (this.questInfo.questCode == "qst_008")
            {
                //QuestCountManager.instance.ResetInsectDeathCount();
                //QuestCountManager.instance.ResetSkeletonDeathCount();
            }
                
        }
    }

    public void SetQuestClear()
    {
        isQuestGoalArrival = true;
    }

    public void SetQuestActive(bool input)
    {
        isActive = input;

        if (isActive)
        {
            questDetail.questName = QuestDataManager.instance.GetQuest(questInfo.questCode).name;
            questDetail.isSucceed = false;
            questDetail.questGoal = QuestDataManager.instance.GetQuest(questInfo.questCode).goal;
            if (QuestDataManager.instance.GetQuest(questInfo.questCode).type.Contains("Collecting") || QuestDataManager.instance.GetQuest(questInfo.questCode).type.Contains("Hunting"))
            {
                questDetail.isCommon = false;
            }
            else
                questDetail.isCommon = true;
            QuestAlarmManager.instance.AddQuestAlarm(questDetail);
        }
    }

    public void SetQuestAvailable(bool input)
    {
        isQuestAvailable = input;
    }
    public bool GetQuestAvailable()
    {
        return isQuestAvailable;
    }
    public bool GetQuestActive()
    {
        return isActive;
    }

    public bool IsQuestClear()
    {
        return isClear;
    }

    public bool IsQuestGoalArrival()
    {
        return isQuestGoalArrival;
    }
    public void QuestClear()
    {
        if (isQuestGoalArrival && npcFunction.isTalkingPlayerToNPC)
        {
            isClear = true;
            if(isClear)
                npcFunction.SetPlayerClearQuestToNPC(false);
        }
            
    }

    public void SetQuestPlayerControl(PlayerControl input)
    {
        playerControl = input;
    }

    public void SetQuestNPCFunction(NPCFunction input)
    {
        npcFunction = input;
    }
}
