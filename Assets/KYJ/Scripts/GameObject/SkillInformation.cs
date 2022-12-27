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
                print(this.name + " ��� ����");
            }
        }
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    print("1�� ������");
        //    StartCoroutine(CoolTime(data.coolTime));
        //}
    }

    IEnumerator CoolTime (float cool)
    {
        print("��Ÿ�� �ڷ�ƾ ����");

        while(cool > 1.0f)
        {
            cool -= Time.deltaTime;
            print("�ð� : " +1.0f / cool);
            yield return new WaitForFixedUpdate();
        }

        print("��Ÿ�� �ڷ�ƾ �Ϸ�");
    }


}
