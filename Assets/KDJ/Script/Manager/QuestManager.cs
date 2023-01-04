using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
        foreach (QuestDataManager.QuestData questData in QuestDataManager.instance.GetQuestAll())
        {
            Quest quest = new Quest();

            quest.questInfo.questCode = questData.questNum;
            quest.questInfo.questType = questData.type;

            quests.Add(quest);
        }
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
