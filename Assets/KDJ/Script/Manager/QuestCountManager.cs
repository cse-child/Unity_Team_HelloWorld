using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCountManager : MonoBehaviour
{
    static private QuestCountManager _instance;
    static public QuestCountManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("QuestCountManager");
                _instance = obj.AddComponent<QuestCountManager>();
            }

            return _instance;
        }
    }
    public int skeletonDeathCount = 0;
    public int insectDeathCount = 0;
    public int bearDeathCount = 0;

    public GameObject monster;
    List<TraceAI> skeletonsDeathCheck = new List<TraceAI>();
    List<IncectAI> insectsDeathCheck = new List<IncectAI>();
    List<BearAI> bearsDeathCheck = new List<BearAI>();

    public void Start()
    {
        monster = GameObject.Find("Monster");
        OnCountInQuest();
    }

    private void OnCountInQuest()
    {
        int test = 0;
        for (int i = 0; i < monster.transform.childCount; i++)
        {
            GameObject obj = monster.transform.GetChild(i).gameObject;

            if (obj.name.Contains("Skeleton"))
            {
                TraceAI trace = obj.GetComponent<TraceAI>();
                skeletonsDeathCheck.Add(trace);
            }

            if(obj.name.Contains("Bear"))
            {
                BearAI bear = obj.GetComponent<BearAI>();
                bearsDeathCheck.Add(bear);
                Debug.Log(test);
            }

            if(obj.name.Contains("Green"))
            {
                IncectAI insect = obj.GetComponent<IncectAI>();
                insectsDeathCheck.Add(insect);
            }
        }
    }

    public int GetSkeletonsDeathCount()
    {
        foreach(TraceAI temp in skeletonsDeathCheck)
        {
            if(temp.GetState() == TraceAI.State.DEAD)
            {
                skeletonDeathCount++;
            }
        }
        return skeletonDeathCount;
    }

    public int GetInsectsDeathCount()
    {
        foreach (IncectAI temp in insectsDeathCheck)
        {
            if (temp.GetState() == IncectAI.State.DEAD)
            {
                insectDeathCount++;
            }
        }
        return insectDeathCount;
    }

    public int GetBearsDeathCount()
    {
        foreach (BearAI temp in bearsDeathCheck)
        {
            if (temp.GetState() == BearAI.State.DEAD)
            {
                bearDeathCount++;
            }
        }
        return bearDeathCount;
    }

    public void ResetSkeletonDeathCount()
    {
        skeletonDeathCount = 0;
    }
    public void ResetInsectDeathCount()
    {
        insectDeathCount = 0;
    }
    public void ResetBearDeathCount()
    {
        bearDeathCount = 0;
    }

}
