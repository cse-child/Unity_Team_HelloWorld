using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMemoryPool : MonoBehaviour
{
    [SerializeField]
    private GameObject enemySpawnPointPrefab; //등장 전 위치를 알려주는 프리팹
    [SerializeField]
    private GameObject enemyPrefab; //생성되는 오브젝트
    [SerializeField]
    private float enemySpawnTime = 0.5f; //생성 주기
    [SerializeField]
    private float enemySpawnLatency = 1; //등장위치 오브젝트 이후 등장까지 걸리는 시간

    private MemoryPool spawnPointMemoryPool; // 적 등장 위치를 알려주는 오브젝트 생성, 활성/ 비활성 관리
    private MemoryPool enemyMemoryPool; // 적 생성 및 활성/비활성화 관리

    private int numberOfEnemiesSpawnAtOnce = 1; // 동시에 생성되는 적 숫자

    public GameObject spawnArea;
    private Vector2Int mapSize = new Vector2Int(10,10); //맵크기

    private void Awake()
    {
        spawnPointMemoryPool = new MemoryPool(enemySpawnPointPrefab);
        enemyMemoryPool = new MemoryPool(enemyPrefab);

        StartCoroutine("SpawnTile");
    }

    private IEnumerator SpawnTile()
    {
        int currentNumber = 0;
        int maximumNumber = 10;

        while(true)
        {
            for(int i = 0; i < numberOfEnemiesSpawnAtOnce; i++)
            {
                GameObject item = spawnPointMemoryPool.ActivatePoolItem();

                item.transform.position = new Vector3(Random.Range(-mapSize.x * 0.49f, mapSize.x * 0.49f),
                                                      1,
                                                      Random.Range(-mapSize.x * 0.49f, mapSize.x * 0.49f));

                StartCoroutine("SpawnObject", item);

                currentNumber++;

                if(currentNumber >= maximumNumber)
                {
                    currentNumber = 0;
                    numberOfEnemiesSpawnAtOnce++;
                }

                yield return new WaitForSeconds(enemySpawnTime);
            }
        }
    }
    private IEnumerator SpawnObject(GameObject point)
    {
        yield return new WaitForSeconds(enemySpawnLatency);

        // 오브젝트 생성 하고 적의 위치를 point의 위치로 사용
        GameObject item = enemyMemoryPool.ActivatePoolItem();
        item.transform.position = point.transform.position;

        //타일 오브젝트 비활성화
        spawnPointMemoryPool.DeactivatePoolItem(point);
    }
}
