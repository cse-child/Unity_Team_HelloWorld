using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private PlayerState playerState;

    public int skillNum = 0;

    private void Awake()
    {
        playerState = FindObjectOfType<PlayerState>();
    }

    // 파티클 - Enemy 충돌
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Skeleton"))
        {
            float damage = playerState.curAtk + SkillDataManager.instance.GetSkillData(skillNum).damage;

            //other.GetComponent<EnemyControl>().TakeDamage(damage); //테스트 몬스터 함수
            other.GetComponent<TraceAI>().Hurt(damage);
        }
        if (other.CompareTag("Bear"))
        {
            float damage = playerState.curAtk + SkillDataManager.instance.GetSkillData(skillNum).damage;

            //other.GetComponent<EnemyControl>().TakeDamage(damage); //테스트 몬스터 함수
            other.GetComponent<BearAI>().Hurt(damage);
        }
        if (other.CompareTag("Incect"))
        {
            float damage = playerState.curAtk + SkillDataManager.instance.GetSkillData(skillNum).damage;

            //other.GetComponent<EnemyControl>().TakeDamage(damage); //테스트 몬스터 함수
            other.GetComponent<IncectAI>().Hurt(damage);
        }
        //if (other.CompareTag("Dragon"))
        //{
        //    float damage = playerState.curAtk + SkillDataManager.instance.GetSkillData(skillNum).damage;

        //    //other.GetComponent<EnemyControl>().TakeDamage(damage); //테스트 몬스터 함수
        //    other.GetComponent<DragonAI>().Hurt(damage);
        //}
    }
}
