using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    
    public bool isActive = false;

    public struct QuestInfo
    {
        public string questCode;
        public string questType;
    }

    public QuestInfo questInfo;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
