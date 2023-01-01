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
                // 이 방법으로 사용하면 하이어라키 창에 DataManager를 넣지 않아도 됨
                GameObject obj = new GameObject("SkillManager");
                _instance = obj.AddComponent<SkillManager>();
            }
            return _instance;
        }
    }
    const int MAX_SKILL_COUNT = 4;

    public List<SkillInformation> skillInfos = new List<SkillInformation>();

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("1번 눌림쓰");
            //skillInfos[0].PlaySkill();
            StartCoroutine(CoolTime(7.0f));
        }
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

    IEnumerator CoolTime(float cool)
    {
        print("쿨타임 코루틴 실행");

        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            print("시간 : " + 1.0f / cool);
            yield return new WaitForFixedUpdate();
        }

        print("쿨타임 코루틴 완료");
    }
}
