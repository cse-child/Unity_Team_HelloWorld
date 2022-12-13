using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    const int maxHp = 100;
    const int maxMp = 100;
    const float maxExp = 100.0f;

    public int curHp = 100;
    public int curMp = 100;
    public float curExp = 0.0f;
    public int gold = 10000;
}
