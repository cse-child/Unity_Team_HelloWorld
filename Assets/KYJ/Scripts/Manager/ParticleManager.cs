using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    static private ParticleManager _instance;
    public static ParticleManager instance
    {
        get
        {
            if (_instance == null)
            {
                // 이 방법으로 사용하면 하이어라키 창에 DataManager를 넣지 않아도 됨
                GameObject obj = new GameObject("ParticleManager");
                _instance = obj.AddComponent<ParticleManager>();
            }
            return _instance;
        }
    }
    
    private Dictionary<string, GameObject> particles = new Dictionary<string, GameObject>();

    public GameObject swordSkill3;

    public void CreateParticle()
    {
        if(swordSkill3 != null)
        {
            AddParticle("Skill_3", swordSkill3);
        }
    }

    private void AddParticle(string key, GameObject prefab)
    {

        GameObject particle = Instantiate(prefab, transform);
        particle.name = key;
        particle.SetActive(false);
        particles.Add(key, particle);
    }

    public void Play(string key, Vector3 pos, Quaternion rot)
    {
        if (!particles.ContainsKey(key)) return;

        particles[key].SetActive(true);
        particles[key].transform.position = pos;
        particles[key].transform.rotation = rot;
    }
}
