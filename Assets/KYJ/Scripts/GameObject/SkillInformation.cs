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

    public IEnumerator CoolTime ()
    {
        print("SkillInfo의 쿨타임 코루틴 실행 : " + data.coolTime);
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

        print("쿨타임 코루틴 완료");
        available = true;
        waitTime = 0.0f;
    }


}
