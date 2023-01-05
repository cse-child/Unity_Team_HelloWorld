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

    // Weapon - Enemy 충돌
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 타격하는 대상의 태그, 컴포넌트, 함수는 바뀔 수 있다.
        if (other.CompareTag("Enemy"))
        {
            print("Enemy Tag");
            //other.GetComponent<EnemyControl>().TakeDamage(playerState.curAtk); //테스트용 함수
            other.GetComponent<TraceAI>().Hurt(playerState.curAtk);
        }
    }

    private IEnumerator AutoDisable()
    {
        // 0.3f초 후에 오브젝트가 사라짐
        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }
}
