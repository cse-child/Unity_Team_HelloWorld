using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int curHp = 100;

    private void Update()
    {
        if(curHp < 0)
            gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        print("Enemy Damaged! - " +curHp);
    }
}
