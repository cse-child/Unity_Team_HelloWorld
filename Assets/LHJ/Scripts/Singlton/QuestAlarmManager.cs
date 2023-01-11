using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAlarmManager : MonoBehaviour
{
    static public QuestAlarmManager _instance;
    static public QuestAlarmManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("QuestAlarmManager");
                _instance = obj.AddComponent<QuestAlarmManager>();
            }
            return _instance;
        }
    }

    public struct QuestDetail
    {
        public string questName;
        public string questGoal;
        //public string questProgress;
        public bool isCommon;
        public bool isSucceed;
    }

    public List<GameObject> quests = new List<GameObject>();
    public GameObject questListBG;
    public GameObject questList;

    public void Start()
    {
        UIControl uiControl = FindObjectOfType<UIControl>();
        questListBG = uiControl.questListPrefab;
        questList = GameObject.Find("UI").transform.Find("StatsUI").transform.Find("QuestStat").transform.Find("QuestList").gameObject;
    }

    public void AddQuestAlarm(QuestDetail data)
    {
        if (quests.Count != 0)
        {
            foreach (GameObject quest in quests)
            {
                if (!quest.activeSelf)
                {
                    quest.GetComponent<QuestAlarmData>().SetQuestDetail(data);
                    quest.SetActive(true);
                    return;
                }
            }
        }

        GameObject temp = Instantiate(questListBG);
        temp.transform.parent = questList.transform;
        temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        temp.GetComponent<QuestAlarmData>().SetQuestDetail(data);
        quests.Add(temp);
    }

    public void RemoveAlarm(string name)
    {
        foreach (GameObject quest in quests)
        {
            if (quest.GetComponent<QuestAlarmData>().questDetail.questName == name)
            {
                quest.SetActive(false);
            }
        }
    }

    public List<GameObject> GetQuestObjects()
    {
        return quests;
    }

    public GameObject GetQuestGameObject(string name)
    {
        foreach (GameObject quest in quests)
        {
            if(quest.GetComponent<QuestAlarmData>().questDetail.questName == name)
                return quest;
        }
        Debug.Log("퀘스트 알람 데이터 불러올 이름이 잘못됨");
        return null;
    }

    public QuestAlarmData GetQuestAlarmData(string name)
    {
        foreach (GameObject quest in quests)
        {
            if (quest.GetComponent<QuestAlarmData>().questDetail.questName == name)
                return quest.GetComponent<QuestAlarmData>();
        }
        Debug.Log("퀘스트 알람 데이터 불러올 이름이 잘못됨");
        return null;
    }

    public void Non()
    {

    }
}
