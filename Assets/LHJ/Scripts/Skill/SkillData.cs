using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillData : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int skillNum = 0;
    public Image skillImage;
    public SkillUIManager skillUIManager;

    public void OnEnable()
    {
        if (skillUIManager == null)
            skillUIManager = FindObjectOfType<SkillUIManager>();
        if (skillImage == null)
            skillImage = transform.Find("Image").GetComponent<Image>();
        SetSkillIcon();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (skillUIManager == null)
            skillUIManager = FindObjectOfType<SkillUIManager>();
        if (skillImage == null)
            skillImage = transform.Find("Image").GetComponent<Image>();
        SetSkillIcon();
    }

    // Update is called once per frame
    void Update()
    {
        SetSkillIcon();     //ºÒ¾È....

    }

    public void SetSkillIcon()
    {
        if(skillNum == 0)
        {
            //skillImage.sprite = null;
            skillImage.sprite = skillUIManager.lockImage;
            //skillImage.color = new Vector4(255, 255, 255, 0);
            return;
        }
        if (SkillManager.instance.GetSkill(skillNum) == null)
            return;
        if(skillNum != 0 && SkillManager.instance.GetSkill(skillNum).data.learning == false)
            skillImage.sprite = skillUIManager.lockImage;
        else if(skillNum != 0 && SkillManager.instance.GetSkill(skillNum).data.learning == true)
            skillImage.sprite = SkillDataManager.instance.GetSkillData(skillNum).image;
        skillImage.color = new Vector4(255, 255, 255, 255);
    }

    public void SetSkill(int skillNum)
    {
        this.skillNum = skillNum;
        SetSkillIcon();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (skillImage.sprite == skillUIManager.lockImage)
            return;

        if(SkillManager.instance.GetSkill(skillNum).data.learning == true)
            skillUIManager.SetSelectSkill(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (skillImage.sprite == skillUIManager.lockImage)
            return;

        if (skillUIManager.selectHotKeySlot != null)
        {
            skillUIManager.SetHotKeySkill();
        }
    }
}
