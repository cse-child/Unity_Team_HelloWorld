using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCountManager : MonoBehaviour
{
    static private MonsterCountManager _instance;
    static public MonsterCountManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("MonsterCountManager");
                _instance = obj.AddComponent<MonsterCountManager>();
            }

            return _instance;
        }
    }

    public GameObject monster;
    //GameObject obj = GameObject.Find("Skeleton");
    Dictionary<GameObject, TraceAI> skeletons = new Dictionary<GameObject, TraceAI>();
    Dictionary<GameObject, BearAI> bears = new Dictionary<GameObject, BearAI>();
    Dictionary<GameObject, IncectAI> insects = new Dictionary<GameObject, IncectAI>();
    //List<GameObject> bears = new List<GameObject>();
    //List<GameObject> skeletons = new List<GameObject>();
    //List<GameObject> insects = new List<GameObject>();

    public void Start()
    {
        monster = GameObject.Find("Monster");
        LoadTests();
    }

    private void LoadTests()
    {
        for(int i = 0; i < monster.transform.childCount; i++)
        {
            GameObject obj = monster.transform.GetChild(i).gameObject;
            if(obj.name.Contains("Skeleton"))
            {
                TraceAI trace = obj.GetComponent<TraceAI>();
                skeletons.Add(obj, trace);
            }
            if(obj.name.Contains("Green"))
            {
                IncectAI insectAi = obj.GetComponent<IncectAI>();
                insects.Add(obj, insectAi);
            }
            if(obj.name.Contains("Bear"))
            {
                BearAI bearAi = obj.GetComponent<BearAI>();
                bears.Add(obj, bearAi);
            }
            //UnityEngine.Debug.Log(obj.name);
        }
    }

}
