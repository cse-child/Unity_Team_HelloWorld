using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    static private QuestManager _instance;
    static public QuestManager instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject obj = new GameObject("QuestManager");
                _instance = obj.AddComponent<QuestManager>();
            }

            return _instance;
        }
    }

    public struct QuestData
    {
        public string questCode;
        public string questName;
        public string questType;
        public string questGoal;
        public string questDetail;
        public string questAchivement;
        public string questLevelRequire;
    }

    List<QuestData> quests = new List<QuestData>();

    public void Awake()
    {
        LoadQuestData();
    }

    private void LoadQuestData()
    {
        string temp = System.IO.File.ReadAllText("Assets/KDJ/TextData/QuestData.csv");
        temp = temp.Replace("\r\n", "\n");
        string[] row = temp.Split("\n");
        for(int i = 1; i < row.Length; i++)
        {
            string[] col = row[i].Split(",");
            //string[] questValue = col[2].Split("_");
            //string[] questAchieveValue = col[5].Split("_");

            QuestData questData;
            questData.questCode = col[0];
            questData.questName = col[1];
            questData.questType = col[2];
            questData.questGoal = col[3];
            questData.questDetail = col[4];
            questData.questAchivement = col[5];
            questData.questLevelRequire = col[6];

            quests.Add(questData);
        }
    }

    public QuestData GetQuestData(int num)
    {
        return quests[num - 1];
    }
}
