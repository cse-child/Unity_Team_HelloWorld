using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUIManager : MonoBehaviour
{
    public GameObject selectSkill;
    public HotKeySkillSlot selectHotKeySlot;
    // Start is called before the first frame update
    void Start()
    {
        selectSkill = transform.Find("SelectSkill").gameObject;
        selectSkill.SetActive(true);
        selectSkill.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(selectSkill.gameObject.activeSelf)
        {
            selectSkill.transform.position = Input.mousePosition;

        }

    }

    public void SetSelectSkill(SkillData skillData)
    {
        selectSkill.GetComponent<SkillData>().SetSkill(skillData.skillNum);
        if (selectSkill.GetComponent<SkillData>().skillNum != 0)
            selectSkill.gameObject.SetActive(true);
    }

    public void SetHotKeySkill()
    {
        selectHotKeySlot.SetSkillNum(selectSkill.GetComponent<SkillData>().skillNum);
        selectSkill.gameObject.SetActive(false);
    }

    public void SetHotKey(HotKeySkillSlot hotkey)
    {
        selectHotKeySlot = hotkey;
    }

    public void RemoveHotKey()
    {
        selectHotKeySlot = null;
    }
}
