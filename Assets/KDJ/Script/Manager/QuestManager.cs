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

    public List<List<int>> questData = new List<List<int>>();

    public void LoadQuestData()
    {

    }
}
