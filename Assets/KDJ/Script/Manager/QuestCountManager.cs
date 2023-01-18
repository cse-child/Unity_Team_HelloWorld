using Language.Lua;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

    delegate void EventHandler();

    public bool isSwitchOn = false;
    public int bearDeathCount = 0;


    public GameObject monster;
    List<TraceAI> skeletonsDeathCheck = new List<TraceAI>();
    List<IncectAI> insectsDeathCheck = new List<IncectAI>();
    List<BearAI> bearsDeathCheck = new List<BearAI>();

    List<bool> deathCheckList = new List<bool>();
    //Dictionary<BearAI, bool> bearsDeathCheck = new Dictionary<BearAI, bool>();
    //Dictionary<List<BearAI>, bool> bearsDeathChecks = new Dictionary<bool, BearAI>();
    public void Awake()
    {
        monster = GameObject.Find("Monster");
    }

    public void Start()
    {
        OnCountInQuest();
    }

    private void Update()
    {
        ScanBearsDeathCount();
        for (int i = 0; i < bearsDeathCheck.Count; i++)
        {
            if (bearsDeathCheck[i].GetState().Equals(BearAI.State.DEAD) && deathCheckList[i] == true)
            {
                bearDeathCount++;
                return;
            }
        }
    }

    private void OnCountInQuest()
    {
        for (int i = 0; i < monster.transform.childCount; i++)
        {
            GameObject obj = monster.transform.GetChild(i).gameObject;
            bool deathCheck = false;
            if (obj.name.Contains("Skeleton"))
            {
                TraceAI trace = obj.GetComponent<TraceAI>();
                if (trace != null)
                {
                    skeletonsDeathCheck.Add(trace);
                }
            }

            if(obj.name.Contains("Bear"))
            {
                BearAI bear = obj.GetComponent<BearAI>();
                if (bear != null)
                    bearsDeathCheck.Add(bear);
                deathCheckList.Add(deathCheck);
            }

            if(obj.name.Contains("Green"))
            {
                IncectAI insect = obj.GetComponent<IncectAI>();
                insectsDeathCheck.Add(insect);
            }

        }
        UnityEngine.Debug.Log(monster.transform.childCount.ToString());
        UnityEngine.Debug.Log(bearsDeathCheck.Count.ToString());
        Test();
    }

    public void ScanSkeletonsDeathCount()
    {
        foreach(TraceAI temp in skeletonsDeathCheck)
        {
            if(temp.GetState().Equals(TraceAI.State.DEAD))
            {
                //skeletonDeathCount++;
                break;
            }
        }
    }

    public void Test()
    {
        string test;
        foreach (BearAI temp in bearsDeathCheck)
        {
            if (temp.GetState().Equals(BearAI.State.DEAD))
                test = "dead";
            else
                test = "No";

            UnityEngine.Debug.Log(test);
        }
    }
    public void ScanInsectsDeathCount()
    {
        foreach (IncectAI temp in insectsDeathCheck)
        {
            if (temp.GetState().Equals(IncectAI.State.DEAD))
            {
                //insectDeathCount++;
            }
            else
                continue;
        }
    }

    public void ScanBearsDeathCount()
    {
        for(int i = 0; i < bearsDeathCheck.Count; i++)
        {
            //if (bearsDeathCheck[i].onDie.EndInvoke(new IAsyncResult))

            if (bearsDeathCheck[i].GetState().Equals(BearAI.State.DEAD))
                deathCheckList[i] = true;
            else
                deathCheckList[i] = false;
        }
        //for (int i = 0; i < bearsDeathCheck.Count; i++)
        //{
        //    if (bearsDeathCheck[i].GetState().Equals(BearAI.State.DEAD) && deathCheckList[i] == true)
        //    {
        //        isSwitchOn = true;
        //        return;
        //    }
        //    else
        //    {
        //        isSwitchOn = false;
        //        return;
        //    }
        //}
    }

    private void AddCount()
    {
        
    }
    //public int GetSkeletonsDeathCount()
    //{
    //    return skeletonDeathCount;
    //}
    public int GetBearsDeathCount()
    {
        return bearDeathCount;
    }
    //public int GetInsectsDeathCount()
    //{
    //    return insectDeathCount;
    //}

//    //public void ResetSkeletonDeathCount()
    //{
    //    skeletonDeathCount = 0;
    //}
    //public void ResetInsectDeathCount()
    //{
    //    insectDeathCount = 0;
    //}
    //public void ResetBearDeathCount()
    //{
    //    bearDeathCount = 0;
    //}

    public bool GetSwitchOn()
    {
        return isSwitchOn;
    }
    public void SetSwitchOn(bool input)
    {
        isSwitchOn = input;
    }
}
