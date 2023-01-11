using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Quest
{
    
    public bool isActive = false;
    public bool isClear = false;

    public bool isQuestGoalArrival = false;

    public GameObject player;
    public GameObject enemy;

    private int countValue = 0;

    public struct QuestInfo
    {
        public string questCode;
        public string questType;
    }

    public QuestInfo questInfo;

    private string type;
    private int requireCount;

    private QuestAlarmManager.QuestDetail questDetail;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        if(this.questInfo.questCode == "qst001")
        {
            Vector3 temp = player.transform.position;
            if (player.transform.position == temp)
                isQuestGoalArrival = true;
        }
        else if(this.questInfo.questCode == "qst002")
        {
            isQuestGoalArrival = true;
            QuestAlarmManager.instance.GetQuestAlarmData(questDetail.questName).questDetail.isSucceed = true;
        }
        else if(this.questInfo.questCode == "qst003")
        {

        }
    }

    private void Collecting()
    {
        string itemCode;
        if (this.questInfo.questCode == "qst_006")
            //itemCode = 

            //PlayerInventoryData.instance.GetHasInventory()[]

        if (countValue >= requireCount)
        {
            isQuestGoalArrival = true;
            QuestAlarmManager.instance.GetQuestAlarmData(questDetail.questName).questDetail.isSucceed = true;
        }

    }

    private void Researching()
    {
        //if (this.)
    }

    private void Hunting()
    {
        countValue++;

        if (countValue >= requireCount)
        {
            isQuestGoalArrival = true;
            QuestAlarmManager.instance.GetQuestAlarmData(questDetail.questName).questDetail.isSucceed = true;
        }
    }

    public void SetQuestClear()
    {
        isQuestGoalArrival = true;
    }

    public void SetQuestActive(bool input)
    {
        isActive = input;

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

    public bool GetQuestActive()
    {
        return isActive;
    }

    public bool IsQuestClear()
    {
        return isClear;
    }

    public void QuestClear()
    {
        if (isQuestGoalArrival) 
            isClear = true;
    }
}
