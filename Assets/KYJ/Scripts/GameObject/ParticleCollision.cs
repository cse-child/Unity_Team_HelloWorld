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
        if (other.CompareTag("Enemy"))
        {
            float damage = playerState.curAtk + SkillDataManager.instance.GetSkillData(skillNum).damage;

            //other.GetComponent<EnemyControl>().TakeDamage(damage); //�׽�Ʈ ���� �Լ�
            other.GetComponent<TraceAI>().Hurt(damage);
        }
    }
}
