using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public AudioSource[] sfxPlayer;
    public AudioClip[] sfxClip;
    
    public enum Sfx
    { 
        ATTACK_1, ATTACK_2, ATTACK_3, ATTACK_4, 
        SKILL_1, SKILL_2, SKILL_3, SKILL_4,
        DAMAGE, DEATH,
        EQUIP, UNEQUIP
    };
    private int sfxCursor = 0;
    private float baseVolume = 0.2f;

    private void Awake()
    {
        ParticleManager.instance.CreateParticle();
        SkillDataManager.instance.LoadItemData();
        SkillManager.instance.InitSkillInfos();
    }

    public void SfxPlay(Sfx type)
    {
        sfxPlayer[sfxCursor].volume = baseVolume; // º¼·ý ÃÊ±âÈ­
        print(type);
        switch(type)
        {
            case Sfx.ATTACK_1:
                sfxPlayer[sfxCursor].clip = sfxClip[0];
                break;
            case Sfx.ATTACK_2:
                sfxPlayer[sfxCursor].clip = sfxClip[1];
                break;
            case Sfx.ATTACK_3:
                sfxPlayer[sfxCursor].clip = sfxClip[2];
                break;
            case Sfx.ATTACK_4:
                sfxPlayer[sfxCursor].clip = sfxClip[3];
                break;
            case Sfx.SKILL_1:
                sfxPlayer[sfxCursor].clip = sfxClip[4];
                sfxPlayer[sfxCursor].volume = 0.1f;
                break;
            case Sfx.SKILL_2:
                sfxPlayer[sfxCursor].clip = sfxClip[5];
                sfxPlayer[sfxCursor].volume = 0.1f;
                break;
            case Sfx.SKILL_3:
                sfxPlayer[sfxCursor].clip = sfxClip[6];
                break;
            case Sfx.SKILL_4:
                sfxPlayer[sfxCursor].clip = sfxClip[7];
                break;
            case Sfx.DAMAGE:
                sfxPlayer[sfxCursor].clip = sfxClip[Random.Range(8, 13)];
                break;
            case Sfx.DEATH:
                sfxPlayer[sfxCursor].clip = sfxClip[13];
                break;
            case Sfx.EQUIP: // 10
                sfxPlayer[sfxCursor].clip = sfxClip[14];
                break;
            case Sfx.UNEQUIP: // 11
                sfxPlayer[sfxCursor].clip = sfxClip[15];
                break;
        }
        sfxPlayer[sfxCursor].Play();
        sfxCursor = (sfxCursor+1) % sfxPlayer.Length;
    }
}
