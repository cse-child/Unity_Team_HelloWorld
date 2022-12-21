using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    private void OnEnable()
    {
        //print("Attack Collision OnEnable !");
        StartCoroutine("AutoDisable");
    }

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ÿ���ϴ� ����� �±�, ������Ʈ, �Լ��� �ٲ� �� �ִ�.
        if (other.CompareTag("Enemy"))
            other.GetComponent<EnemyControl>().TakeDamage(10);
    }

    private IEnumerator AutoDisable()
    {
        // 1�� �Ŀ� ������Ʈ�� �����
        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }
}
