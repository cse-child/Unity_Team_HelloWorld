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
        // 스킬 단축키 설정하기 예시 !!
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

    /* 스킬을 실행시키는 함수 - 스킬 실행의 첫 단계를 담당 */
    private void PlaySkills()
    {
        // 무기를 들고있지 않은 경우 스킬 사용X
        if (playerControl.WeaponState() == 0) return;
        // 스킬 상태가 0이 아닌 경우 -> 다른 스킬을 사용중인 경우 스킬 사용X
        if (playerControl.SkillState() != 0) return;

        foreach (SkillInformation info in skillInfos)
        {
            if (Input.GetKeyDown(info.GetKeyCode()) && info.available)
            {
                /* Player MP 감소 */
                if (playerState.curMp < info.data.decreaseMP)
                {
                    print("MP가 부족하여 스킬을 사용할 수 없습니다. \n현재 MP : " + playerState.curMp +" / 필요 MP : "+info.data.decreaseMP);
                    return; // MP 부족하면 스킬 사용X
                }
                else
                    playerState.curMp -= info.data.decreaseMP;

                // 스킬에 버프 능력이 있는 경우 공격력 증가 적용
                int buff = SkillDataManager.instance.GetSkillData(info.skillNum).buff;
                if (buff != 0)
                {
                    playerState.curAtk += buff;
                    StartCoroutine(info.DurationTime(buff));
                }

                /* Cool Time 코루틴 시작 */
                StartCoroutine(info.CoolTime());

                /* Skill Animation 실행 */
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

    // Level에 따라 스킬을 배울 수 있는지 없는지 체크
    private void CheckLearningSkill()
    {
        foreach (SkillInformation info in skillInfos)
        {
            if (playerState.level <= info.data.level)
                info.data.learning = true;
        }
    }
}
