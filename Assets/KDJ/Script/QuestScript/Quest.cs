using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    
    public bool isActive = false;
    public bool isClear = false;

    public GameObject player;
    public GameObject enemy;

    public struct QuestInfo
    {
        public string questCode;
        public string questType;
    }

    public QuestInfo questInfo;

    private string type;
    private int requireCount;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        TypeDefinition();
    }

    private void Update()
    {
        if (!isActive) return;

        QuestApart();
    }

    private void TypeDefinition()
    {
        if (!questInfo.questType.Contains("_"))
            type = questInfo.questType;
        else
        {
            string[] temp = questInfo.questType.Split("_");
            type = temp[0];
            requireCount = int.Parse(temp[1]);
        }
    }

    private void QuestApart()
    {
        switch (type)
        {
            case "Tutorial":
                Tutorial();
                return;
            case "Hunting":
                Hunting();
                return;
            case "Collecting":
                Collecting();
                return;
            case "Researching":
                Researching();
                return;
        }
    }

    private void Tutorial()
    {

    }

    private void Collecting()
    {

    }

    private void Researching()
    {
        
    }

    private void Hunting()
    {
        
    }

    public void SetQuestActive(bool input)
    {
        isActive = input;
    }

    public void QuestClear()
    {
        isClear = true;

        if (isClear)
            isActive = false;
    }
}
