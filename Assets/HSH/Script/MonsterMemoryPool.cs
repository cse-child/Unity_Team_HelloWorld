using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMemoryPool : MonoBehaviour
{
    [SerializeField]
    private GameObject enemySpawnPointPrefab; //���� �� ��ġ�� �˷��ִ� ������
    [SerializeField]
    private GameObject enemyPrefab; //�����Ǵ� ������Ʈ
    [SerializeField]
    private float enemySpawnTime = 0.5f; //���� �ֱ�
    [SerializeField]
    private float enemySpawnLatency = 1; //������ġ ������Ʈ ���� ������� �ɸ��� �ð�

    private MemoryPool spawnPointMemoryPool; // �� ���� ��ġ�� �˷��ִ� ������Ʈ ����, Ȱ��/ ��Ȱ�� ����
    private MemoryPool enemyMemoryPool; // �� ���� �� Ȱ��/��Ȱ��ȭ ����

    private int numberOfEnemiesSpawnAtOnce = 1; // ���ÿ� �����Ǵ� �� ����

    public GameObject spawnArea;
    private Vector2Int mapSize = new Vector2Int(10,10); //��ũ��

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

        // ������Ʈ ���� �ϰ� ���� ��ġ�� point�� ��ġ�� ���
        GameObject item = enemyMemoryPool.ActivatePoolItem();
        item.transform.position = point.transform.position;

        //Ÿ�� ������Ʈ ��Ȱ��ȭ
        spawnPointMemoryPool.DeactivatePoolItem(point);
    }
}
