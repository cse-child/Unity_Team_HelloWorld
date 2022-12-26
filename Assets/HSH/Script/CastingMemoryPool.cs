using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingMemoryPool : MonoBehaviour
{
    [SerializeField]
    private GameObject castingPrefeb; // 오브젝트
    private MemoryPool memoryPool; //오브젝트 메모리 풀

    private void Awake()
    {
        memoryPool = new MemoryPool(castingPrefeb);
    }

    public void SpawnCasting(Vector3 position, Vector3 direction)
    {
        GameObject item = memoryPool.ActivatePoolItem();
        item.transform.position = position;
        item.transform.rotation = Random.rotation;
        item.GetComponent<Casting>().Setup(memoryPool, direction);
    }
}
