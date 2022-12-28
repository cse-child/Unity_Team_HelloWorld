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
        if(skillNum != 0)
        {
            data = SkillDataManager.instance.GetSkillData(skillNum);
            if (data.learning)
            {
                available = true;
                print(this.name + " 사용 가능");
            }
        }
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    print("1번 눌림쓰");
        //    StartCoroutine(CoolTime(data.coolTime));
        //}
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
