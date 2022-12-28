using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private void Awake()
    {
        ParticleManager.instance.CreateParticle();
        SkillDataManager.instance.LoadItemData();
    }
}
