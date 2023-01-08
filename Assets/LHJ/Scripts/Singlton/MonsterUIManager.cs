using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUIManager : MonoBehaviour
{
    private static MonsterUIManager _instance;
    public static MonsterUIManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("MonsterUIManager");
                _instance = obj.AddComponent<MonsterUIManager>();
            }
            return _instance;
        }
    }

    public MonsterUI monsterUI;

    // Start is called before the first frame update
    void Start()
    {
        monsterUI = FindObjectOfType<MonsterUI>();
    }

    public void Non()
    {

    }

    public void SetActiveMonsterUI(bool active)
    {
        //monsterUI.SetMonster();
        monsterUI.gameObject.SetActive(active);
    }

    public void monster()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
