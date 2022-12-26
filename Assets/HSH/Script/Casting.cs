using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Casting : MonoBehaviour
{
    [SerializeField]
    private float deactivateTime = 5.0f; // ���� �� ��Ȱ��ȭ �Ǵ� �ð�
    [SerializeField]
    private float castingSpin = 1.0f; // ȸ���ϴ� �ӵ�
    [SerializeField]
    private Rigidbody rigidBody;
    private MemoryPool memoryPool;

    public void Setup(MemoryPool pool, Vector3 direction)
    {
        rigidBody = GetComponent<Rigidbody>();
        memoryPool = pool;

        //�̵��ӵ� �� ȸ�� �ӵ� ����
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