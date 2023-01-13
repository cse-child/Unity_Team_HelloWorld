using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInformation : MonoBehaviour
{
    public int skillNum = 0; // ��ų Ű��
    public bool available = true; // ��ų ��� ���� ����
    public SkillDataManager.SkillData data;
    private KeyCode keyCode; // ����Ű
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
            //print("��Ÿ�� ���� �ð� : " + cool);
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
            //print("��Ÿ�� ���� �ð� : " + cool);
            yield return new WaitForFixedUpdate();
        }

        // Buff ���ӽð��� ������ ���ݷ� �ʱ�ȭ
        SkillManager.instance.ResetCurAtk();
    }
}
