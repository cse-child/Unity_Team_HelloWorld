using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    private PlayerState playerState;

    private void Awake()
    {
        playerState = FindObjectOfType<PlayerState>();
    }

    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }

    // Weapon - Enemy �浹
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ÿ���ϴ� ����� �±�, ������Ʈ, �Լ��� �ٲ� �� �ִ�.
        if (other.CompareTag("Enemy"))
        {
            print("Enemy Tag");
            //other.GetComponent<EnemyControl>().TakeDamage(playerState.curAtk); //�׽�Ʈ�� �Լ�
            other.GetComponent<TraceAI>().Hurt(playerState.curAtk);
        }
    }

    private IEnumerator AutoDisable()
    {
        // 0.3f�� �Ŀ� ������Ʈ�� �����
        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }
}
