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

    }

    public SkillInformation GetSkill(int skillNum)
    {
        foreach(SkillInformation info in skillInfos)
        {
            if (info.skillNum == skillNum)
                return info;
        }
        return null;
    }

    private void PlaySkills()
    {
        if (playerControl.WeaponState() == 0) return;

        foreach (SkillInformation info in skillInfos)
        {
            if (Input.GetKeyDown(info.GetKeyCode()) && info.available)
            {
                // Player MP 감소
                if (playerState.curMp < info.data.decreaseMP)
                {
                    print("MP가 부족하여 스킬을 사용할 수 없습니다.");
                    return; // MP 부족하면 스킬 사용X
                }
                else
                    playerState.curMp -= info.data.decreaseMP;

                // Cool Time 코루틴 시작
                StartCoroutine(info.CoolTime());
                // Skill Animation 실행
                playerControl.PlaySkill(info.data.skillNum);
            }
        }
    }
}
