using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float maxHP = 100;
    public float maxMP = 100;
    public float maxEXP = 100.0f; 

    public float curHp = 100;
    public float curMp = 100;
    public float curExp = 0.0f;

    public float baseAtk = 10.0f;
    public float curAtk = 10.0f;
    public float baseDef = 10.0f;
    public float curDef = 10.0f;

    public int gold = 10000;
    public int level = 1;

    private void Update()
    {
        curHp = Mathf.Clamp(curHp, 0, maxHP);
        curMp = Mathf.Clamp(curMp, 0, maxMP);
    }

    public void DecreaseHp(float value)
    {
        curHp -= value;
    }
}
