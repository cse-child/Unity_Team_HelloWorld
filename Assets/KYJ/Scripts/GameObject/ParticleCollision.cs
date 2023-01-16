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

    // ��ƼŬ - Enemy �浹
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Skeleton"))
        {
            float damage = playerState.curAtk + SkillDataManager.instance.GetSkillData(skillNum).damage;

            //other.GetComponent<EnemyControl>().TakeDamage(damage); //�׽�Ʈ ���� �Լ�
            other.GetComponent<TraceAI>().Hurt(damage);
        }
        if (other.CompareTag("Bear"))
        {
            float damage = playerState.curAtk + SkillDataManager.instance.GetSkillData(skillNum).damage;

            //other.GetComponent<EnemyControl>().TakeDamage(damage); //�׽�Ʈ ���� �Լ�
            other.GetComponent<BearAI>().Hurt(damage);
        }
        if (other.CompareTag("Incect"))
        {
            float damage = playerState.curAtk + SkillDataManager.instance.GetSkillData(skillNum).damage;

            //other.GetComponent<EnemyControl>().TakeDamage(damage); //�׽�Ʈ ���� �Լ�
            other.GetComponent<IncectAI>().Hurt(damage);
        }
        //if (other.CompareTag("Dragon"))
        //{
        //    float damage = playerState.curAtk + SkillDataManager.instance.GetSkillData(skillNum).damage;

        //    //other.GetComponent<EnemyControl>().TakeDamage(damage); //�׽�Ʈ ���� �Լ�
        //    other.GetComponent<DragonAI>().Hurt(damage);
        //}
    }
}
