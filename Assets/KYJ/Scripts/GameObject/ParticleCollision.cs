using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    // 파티클 - Enemy 충돌
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<EnemyControl>().TakeDamage(10);
    }
}
