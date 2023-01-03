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

    List<Quest> quests = new List<Quest>();

    public void Awake()
    { 
        LoadQuest();
    }

    private void LoadQuest()
    {
        //foreach
        //{
        //
        //}

        Quest.QuestInfo questInfo;
        //questInfo.questCode = 
        //questInfo.questType =
    }

    public List<Quest> GetQuests()
    {
        return quests;
    }

    public Quest GetQuest(string questCode)
    {
        foreach (Quest quest in quests)
        {
            if (questCode == quest.questInfo.questCode)
                return quest;
        }

        return null;
    }
}
