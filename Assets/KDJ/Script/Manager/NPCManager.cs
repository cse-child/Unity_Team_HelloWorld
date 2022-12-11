using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    enum ObjectType
    {
        NPC, MOVE_RANGE_BOX
    }

    private GameObject npcPrefab;
    private GameObject npcMoveRangeBox;
    private void Awake()
    {
        npcPrefab = Resources.Load<GameObject>("KDJ/Prefabs/NPCSample");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
