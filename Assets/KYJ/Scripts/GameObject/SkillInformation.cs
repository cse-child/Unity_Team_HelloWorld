using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInformation : MonoBehaviour
{
    public int skillNum = 0;
    public bool available = true;
    public SkillDataManager.SkillData data;
    private KeyCode keyCode;
    private float waitTime = 0.0f;

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
        print("SkillInfo의 쿨타임 코루틴 실행 : " + data.coolTime);
        available = false;

        float cool = data.coolTime;
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            //print("쿨타임 남은 시간 : " + cool);
            waitTime = cool;
            yield return new WaitForFixedUpdate();
        }

        print("쿨타임 코루틴 완료");
        available = true;
        waitTime = 0.0f;
    }

    public IEnumerator DurationTime(float time)
    {
        print("Buff 지속시간 코루틴 실행 : " + time);
        available = false;
        
        while (time > 1.0f)
        {
            time -= Time.deltaTime;
            //print("쿨타임 남은 시간 : " + cool);
            yield return new WaitForFixedUpdate();
        }

        // Buff 지속시간이 끝나면 공격력 초기화
        print("Buff 지속시간 끝!");
        SkillManager.instance.ResetCurAtk();
    }
}
