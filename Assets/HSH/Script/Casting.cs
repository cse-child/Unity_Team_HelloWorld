using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Casting : MonoBehaviour
{
    [SerializeField]
    private float deactivateTime = 5.0f; // 등장 후 비활성화 되는 시간
    [SerializeField]
    private float castingSpin = 1.0f; // 회전하는 속도
    [SerializeField]
    private Rigidbody rigidBody;
    private MemoryPool memoryPool;

    public void Setup(MemoryPool pool, Vector3 direction)
    {
        rigidBody = GetComponent<Rigidbody>();
        memoryPool = pool;

        //이동속도 및 회전 속도 설정
        rigidBody.velocity = new Vector3(0, direction.y, 0);
        rigidBody.angularVelocity = new Vector3(Random.Range(-castingSpin, castingSpin),
                                                Random.Range(-castingSpin, castingSpin),
                                                Random.Range(-castingSpin, castingSpin));

        StartCoroutine("DeactivateAfterTime");
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(deactivateTime);

        memoryPool.DeactivatePoolItem(this.gameObject);
    }
}