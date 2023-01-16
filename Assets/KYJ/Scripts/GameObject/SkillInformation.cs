using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInformation : MonoBehaviour
{
    public int skillNum = 0; // 스킬 키값
    public bool available = true; // 스킬 사용 가능 상태
    public SkillDataManager.SkillData data;
    private KeyCode keyCode; // 단축키
    private float waitTime = 0.0f;
    public float coolTime = 0.0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void Init(int skillNum)
    {
        this.skillNum = skillNum;
        data = SkillDataManager.instance.GetSkillData(skillNum);
    }

    public void SetKeyCode(KeyCode key)
    {
        keyCode = key;
    }

    public KeyCode GetKeyCode()
    {
        return keyCode;
    }

    public IEnumerator CoolTime()
    {
        available = false;

        float cool = data.coolTime;
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            //print("쿨타임 남은 시간 : " + cool);
            waitTime = 1.0f / cool;
            coolTime = 1.0f - waitTime;
            yield return new WaitForFixedUpdate();
        }

        available = true;
        waitTime = 0.0f;
    }

    public IEnumerator DurationTime(float time)
    {
        available = false;

        while (time > 1.0f)
        {
            time -= Time.deltaTime;
            //print("쿨타임 남은 시간 : " + cool);
            yield return new WaitForFixedUpdate();
        }

        // Buff 지속시간이 끝나면 공격력 초기화
        SkillManager.instance.ResetCurAtk();
    }
}
