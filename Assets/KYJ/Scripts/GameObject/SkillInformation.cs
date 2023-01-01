using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInformation : MonoBehaviour
{
    public int skillNum = 0;
    public bool available = false;
    public SkillDataManager.SkillData data;

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
        print(skillNum + " 스킬 초기화 완료~");
    }

    public void PlaySkill()
    {
        StartCoroutine(CoolTime(3.0f));
    }

    IEnumerator CoolTime (float cool)
    {
        print("쿨타임 코루틴 실행");

        while(cool > 1.0f)
        {
            cool -= Time.deltaTime;
            print("시간 : " +1.0f / cool);
            yield return new WaitForFixedUpdate();
        }

        print("쿨타임 코루틴 완료");
    }


}
