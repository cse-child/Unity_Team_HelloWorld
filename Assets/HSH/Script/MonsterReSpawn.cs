using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterReSpawn : MonoBehaviour
{
    static private MonsterReSpawn _instance;
    public static MonsterReSpawn instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("MonsterReSpawn");
                _instance = obj.AddComponent<MonsterReSpawn>();
            }
            return _instance;
        }
    }

    public void ReSpawn(GameObject monster)
    {
        StartCoroutine(Spawn(monster));
    }

    IEnumerator Spawn(GameObject monster)
    {
        yield return new WaitForSeconds(7.0f);
        monster.SetActive(true);

    }
}
