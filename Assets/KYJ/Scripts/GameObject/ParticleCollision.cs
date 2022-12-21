using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    // ��ƼŬ - Enemy �浹
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<EnemyControl>().TakeDamage(10);
    }
}
