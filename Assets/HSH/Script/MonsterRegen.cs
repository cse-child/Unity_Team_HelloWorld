using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRegen : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; //생성되는 오브젝트
    [SerializeField]
    private float enemySpawnTime = 3.0f; //생성 주기 체크 후 생성

    private MemoryPool enemyMemoryPool; // 적 생성 및 활성/비활성화 관리

    private int numberOfEnemiesSpawnAtOnce = 5; // 동시에 생성되는 적 숫자
    private int curMonsterCount;
    public GameObject spawnArea;
    private Vector3 mapSize; //맵크기

    private void Awake()
    {
        enemyMemoryPool = new MemoryPool(enemyPrefab);
        mapSize = spawnArea.transform.localScale;
        SpawnMonster();
    }

    private void Update()
    {
        enemyMemoryPool.MonsterRegen();
    }

    private IEnumerator SpawnTile()
    {
        while(true)
        {
            for(int i = 0; i < numberOfEnemiesSpawnAtOnce; ++i)
            {
                GameObject item = enemyMemoryPool.ActivateLimitePoolItem();

                item.transform.position = new Vector3(Random.Range(-mapSize.x * 0.49f, mapSize.z * 0.49f),
                                                      1,
                                                      Random.Range(-mapSize.x * 0.49f, mapSize.z * 0.49f));
                yield return new WaitForSeconds(enemySpawnTime);
            }

            //StopCoroutine("SpawnTile");
        }
    }
    private void SpawnMonster()
    {
        for (int i = 0; i < numberOfEnemiesSpawnAtOnce; ++i)
        {
            GameObject item = enemyMemoryPool.ActivateLimitePoolItem();

            Vector3 curPosition = new Vector3(Random.Range(-mapSize.x, mapSize.z),
                                                  0,
                                                  Random.Range(-mapSize.x, mapSize.z));

            item.transform.position = gameObject.transform.position + curPosition;
        }
    }
    private void AreaExploration()
    {
        
    }
}

