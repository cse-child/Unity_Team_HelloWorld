using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRegen : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; //�����Ǵ� ������Ʈ
    [SerializeField]
    private float enemySpawnTime = 3.0f; //���� �ֱ� üũ �� ����

    private MemoryPool enemyMemoryPool; // �� ���� �� Ȱ��/��Ȱ��ȭ ����

    private int numberOfEnemiesSpawnAtOnce = 5; // ���ÿ� �����Ǵ� �� ����
    private int curMonsterCount;
    public GameObject spawnArea;
    private Vector3 mapSize; //��ũ��

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

