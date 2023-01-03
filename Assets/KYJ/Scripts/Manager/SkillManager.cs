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
    }

    private void PlaySkills()
    {
        if (playerControl.WeaponState() == 0) return;

        foreach (SkillInformation info in skillInfos)
        {
            if (Input.GetKeyDown(info.GetKeyCode()) && info.available)
            {
                // Player MP ����
                if (playerState.curMp < info.data.decreaseMP)
                {
                    print("MP�� �����Ͽ� ��ų�� ����� �� �����ϴ�.");
                    return; // MP �����ϸ� ��ų ���X
                }
                else
                    playerState.curMp -= info.data.decreaseMP;

                // Cool Time �ڷ�ƾ ����
                StartCoroutine(info.CoolTime());
                // Skill Animation ����
                playerControl.PlaySkill(info.data.skillNum);
            }
        }
    }
}
