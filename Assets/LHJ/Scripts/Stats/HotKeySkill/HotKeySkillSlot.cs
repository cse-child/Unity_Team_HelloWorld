using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotKeySkillSlot : MonoBehaviour, IPointerEnterHandler
{
    private KeyCode commend;
    private int skillNum;
    private Image skillImage;
    private Image skillCoolDownImage;
    public UIControl UIControl;
    public SkillUIManager skillUIManager;

    // Start is called before the first frame update
    void Start()
    {
        UIControl = FindObjectOfType<UIControl>();
        skillUIManager = UIControl.SkillUI.GetComponent<SkillUIManager>();
        skillImage = transform.Find("SkillImage").GetComponent<Image>();
        skillCoolDownImage = transform.Find("CoolTime").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (skillNum != 0)
        {
            skillCoolDownImage.fillAmount =   SkillManager.instance.GetSkill(skillNum).coolTime;
        }
    }

    public void SetCommend(KeyCode key)
    {
        commend = key;
    }

    public void SetSkillIcon()
    {
        if (skillNum == 0)
        {
            skillImage.sprite = null;
            skillCoolDownImage.sprite = null;
            skillImage.color = new Vector4(255, 255, 255, 0);
            return;
        }
        skillImage.sprite = SkillDataManager.instance.GetSkillData(skillNum).image;
        skillImage.color = new Vector4(255, 255, 255, 255);
        skillCoolDownImage.sprite = skillImage.sprite;
    }

    public void SetSkillNum(int skillNum)
    {
        this.skillNum = skillNum;
        SetSkillIcon();
        if (skillNum != 0)
            SkillManager.instance.GetSkill(skillNum).SetKeyCode(commend);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        skillUIManager.SetHotKey(this);
    }

}
