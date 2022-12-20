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
        // 플레이어가 타격하는 대상의 태그, 컴포넌트, 함수는 바뀔 수 있다.
        if (other.CompareTag("Enemy"))
            other.GetComponent<EnemyControl>().TakeDamage(10);
    }

    private IEnumerator AutoDisable()
    {
        // 1초 후에 오브젝트가 사라짐
        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }
}
