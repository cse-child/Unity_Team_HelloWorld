using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAlarmData : MonoBehaviour
{
    public QuestAlarmManager.QuestDetail questDetail;
    public Text questNameText;
    public Text goalNameText;
    public string state;
    public string goalData;

    public void Start()
    {
        questNameText = transform.Find("QuestName").GetComponent<Text>();
        goalNameText = transform.Find("Goal").GetComponent<Text>();
    }

    public void SetQuestDetail(QuestAlarmManager.QuestDetail data)
    {
        questDetail = data;
        if (data.isCommon)
            goalData = "진행중";
    }

    public void Update()
    {
        if (questDetail.isSucceed == true)
            goalData = "완료";
        questNameText.text = questDetail.questName;
        goalNameText.text = questDetail.questGoal + "( " + goalData + " )";

    }


}
