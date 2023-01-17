using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float maxHP = 100;
    public float maxMP = 100;
    public float maxEXP = 100.0f;
    public int maxLevel = 10;

    public float curHp = 100;
    public float curMp = 100;
    public float curExp = 0.0f;

    public float baseAtk = 10.0f;
    public float curAtk = 10.0f;
    public float baseDef = 10.0f;
    public float curDef = 10.0f;

    public int gold = 10000;
    public int level = 1;
    public LevelUpUI levelUpUI;

    private PlayerControl playerControl;

    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    private void Update()
    {
        curHp = Mathf.Clamp(curHp, 0, maxHP);
        curMp = Mathf.Clamp(curMp, 0, maxMP);

    }

    public void DecreaseHp(float value)
    {
        curHp -= value;
    }

    public void IncreaseExp(float value)
    {
        curExp += value;
        if(curExp >= maxEXP) // 레벨업 한다!
        {
            curExp -= maxEXP;
            level++;
            level = Mathf.Clamp(level, 1, maxLevel);
            playerControl.PlaySkillEffect("LevelUp");
            playerControl.PlaySfxSound(12);

            levelUpUI.LevelUp(level);
            SetLevelUpState();
        }
    }

    public void ResetState()
    {
        curHp = maxHP;
        curMp = maxMP;
    }

    // 레벨업에 따라 능력치 상승
    private void SetLevelUpState()
    {
        maxHP += LevelUpDataManager.instance.GetLevelUpData(level).maxHp;
        maxMP += LevelUpDataManager.instance.GetLevelUpData(level).maxMp;
        baseAtk += LevelUpDataManager.instance.GetLevelUpData(level).baseAtk;
        baseDef += LevelUpDataManager.instance.GetLevelUpData(level).baseDef;
        curHp = maxHP;
        curMp = maxMP;
        if (!SkillManager.instance.isBuff) // 버프상태가 아닐때만 현재 공격력 변경
            curAtk += LevelUpDataManager.instance.GetLevelUpData(level).baseAtk;
        curDef += LevelUpDataManager.instance.GetLevelUpData(level).baseDef;
        //curDef = baseDef;
    }
}
