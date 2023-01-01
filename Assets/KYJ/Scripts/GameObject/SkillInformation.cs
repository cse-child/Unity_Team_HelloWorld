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
        print(skillNum + " ��ų �ʱ�ȭ �Ϸ�~");
    }

    public void PlaySkill()
    {
        StartCoroutine(CoolTime(3.0f));
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
