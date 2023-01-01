using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HPEvent : UnityEngine.Events.UnityEvent<int, int> { }

public class PlayerHpTest : MonoBehaviour
{
    [HideInInspector]
    public HPEvent onHpEvent = new HPEvent();

    public int maxHP = 100;
    public float maxMP = 100;
    public float maxEXP = 100.0f;

    public int curHp = 100;
    public float curMp = 100;
    public float curExp = 0.0f;
    public int gold = 10000;

    public int CurrentHP => curHp;
    public int MaxHP => maxHP;

    private void Awake()
    {
        curHp = maxHP;
    }
    public bool DecreaseHp(int value)
    {
        int previous = curHp; 

        curHp = curHp - value > 0 ? curHp - value : 0;

        onHpEvent.Invoke(previous, curHp);

        if(curHp == 0)
        {
            return true;
        }
        return false;
    }
    public void Hurt()
    {
        curHp--;
        print(curHp);
        if (curHp <= 0)
            Destroy(gameObject);
    }
}
