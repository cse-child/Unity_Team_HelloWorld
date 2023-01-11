using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDataManager : MonoBehaviour
{
    static public QuestDataManager _instance;
    static public QuestDataManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("QuestDataManager");
                _instance = obj.AddComponent<QuestDataManager>();
            }
            return _instance;
        }
    }

    public struct QuestData
    {
        public string questNum;
        public string name;
        public string type;
        public string goal;
        public string detail;
        //public Dictionary<int,int> achivement;
        public string achivement;
        public int require_Level;

    }

    /*----------------------------변수------------------------*/

    private QuestUIControl questUIControl;
    List<QuestData> quests = new List<QuestData>();

    /*---------------------------------------------------------*/

    public List<QuestData> GetQuestAll()
    {
        return quests;
    }

    public void Awake()
    {
        LoadQuestData(); 
    }

    private void LoadQuestData()
    {
        string temp = System.IO.File.ReadAllText("Assets/LHJ/Resource/TextData/QuestData.csv");
        temp = temp.Replace("\r\n", "\n");
        string[] row = temp.Split("\n");
        for (int i = 1; i < row.Length; i++)
        {
            string[] col = row[i].Split(",");

            QuestData tempData;
            tempData.questNum = col[0];
            tempData.name = col[1];
            tempData.type = col[2];
            tempData.goal = col[3];
            tempData.detail = col[4];
            tempData.achivement = col[5];

            if (col[6] != "제한 없음")
                tempData.require_Level = int.Parse(col[6]);
            else
                tempData.require_Level = 0;

            quests.Add(tempData);
        }
    }

    public QuestData GetQuest(string questNum)
    {
        QuestData NULLQuest;
        NULLQuest.questNum = "";
        NULLQuest.name = "";
        NULLQuest.type = "";
        NULLQuest.goal = "";
        NULLQuest.detail = "";
        NULLQuest.achivement = "";
        NULLQuest.require_Level = 0;
        foreach (QuestData quest in quests)
        {
            if (questNum == quest.questNum)
                return quest;
        }
        return NULLQuest;
    }

    public void SetQuestUIControl(QuestUIControl controlor)
    {
        questUIControl = controlor;
    }

    public void AddQuest(string QuestNum)
    {
        questUIControl.AddQuest(QuestNum);
    }

    public void ClearQuest(string QuestNum)
    {
        questUIControl.ClearQuest(QuestNum);
        QuestAlarmManager.instance.RemoveAlarm(GetQuest(QuestNum).name);
    }
}
