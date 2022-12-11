using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCFunction : MonoBehaviour
{
    private GameObject NPCMoveRangeBox;

    private bool isTalkingPlayerToNPC = false;

    public void SetIsTalkingPlayerToNPC(bool input)
    {
        isTalkingPlayerToNPC = input;
    }

    public bool IsTalkingPlayerToNPC()
    {
        return isTalkingPlayerToNPC;
    }
}
