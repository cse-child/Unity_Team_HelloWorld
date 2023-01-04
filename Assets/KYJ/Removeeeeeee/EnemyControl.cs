using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float curHp = 100.0f;

    private void Update()
    {
        if(curHp <= 0)
            gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;
        print("ÀÔÈù µ¥¹ÌÁö: " + damage + " -> " + this.name+" : " + (int)curHp + " / 100");
    }
}
