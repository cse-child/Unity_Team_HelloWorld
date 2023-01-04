using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    static private SkillManager _instance;
    public static SkillManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("SkillManager");
                _instance = obj.AddComponent<SkillManager>();
            }
            return _instance;
        }
    }
    const int MAX_SKILL_COUNT = 4;

    public List<SkillInformation> skillInfos = new List<SkillInformation>();

    private PlayerControl playerControl;
    private PlayerState playerState;

    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        playerState = FindObjectOfType<PlayerState>();
    }

    private void Update()
    {
        PlaySkills();
        CheckLearningSkill();
    }

    public void InitSkillInfos()
    {
        for (int i = 0; i < MAX_SKILL_COUNT; i++)
        {
            SkillInformation info = new SkillInformation();
            info.Init(i + 1);

            skillInfos.Add(info);
        }
        // ��ų ����Ű �����ϱ� ���� !!
        skillInfos[0].SetKeyCode(KeyCode.Alpha1);
        skillInfos[1].SetKeyCode(KeyCode.Alpha2);
        skillInfos[2].SetKeyCode(KeyCode.Alpha3);
        skillInfos[3].SetKeyCode(KeyCode.Alpha4);
    }

    public SkillInformation GetSkill(int skillNum)
    {
        foreach (SkillInformation info in skillInfos)
        {
            if (info.skillNum == skillNum)
                return info;
        }
        return null;
    }

    /* ��ų�� �����Ű�� �Լ� - ��ų ������ ù �ܰ踦 ��� */
    private void PlaySkills()
    {
        // ���⸦ ������� ���� ��� ��ų ���X
        if (playerControl.WeaponState() == 0) return;
        // ��ų ���°� 0�� �ƴ� ��� -> �ٸ� ��ų�� ������� ��� ��ų ���X
        if (playerControl.SkillState() != 0) return;

        foreach (SkillInformation info in skillInfos)
        {
            if (Input.GetKeyDown(info.GetKeyCode()) && info.available)
            {
                /* Player MP ���� */
                if (playerState.curMp < info.data.decreaseMP)
                {
                    print("MP�� �����Ͽ� ��ų�� ����� �� �����ϴ�. \n���� MP : " + playerState.curMp +" / �ʿ� MP : "+info.data.decreaseMP);
                    return; // MP �����ϸ� ��ų ���X
                }
                else
                    playerState.curMp -= info.data.decreaseMP;

                // ��ų�� ���� �ɷ��� �ִ� ��� ���ݷ� ���� ����
                int buff = SkillDataManager.instance.GetSkillData(info.skillNum).buff;
                if (buff != 0)
                {
                    playerState.curAtk += buff;
                    StartCoroutine(info.DurationTime(buff));
                }

                /* Cool Time �ڷ�ƾ ���� */
                StartCoroutine(info.CoolTime());

                /* Skill Animation ���� */
                playerControl.PlaySkill(info.data.skillNum);
            }
        }
    }

    public bool CheckIsPlaySkill()
    {
        if (playerControl.SkillState() == 0)
            return false;
        else
            return true;
    }

    public void ResetCurAtk()
    {
        playerState.curAtk = playerState.baseAtk;
    }

    // Level�� ���� ��ų�� ��� �� �ִ��� ������ üũ
    private void CheckLearningSkill()
    {
        foreach (SkillInformation info in skillInfos)
        {
            if (playerState.level <= info.data.level)
                info.data.learning = true;
        }
    }
}
