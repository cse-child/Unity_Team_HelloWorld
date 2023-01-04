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


    // Start is called before the first frame update
    void Start()
    {
        skillImage = transform.Find("Image").GetComponent<Image>();
        skillUIManager = FindObjectOfType<SkillUIManager>();
        SetSkillIcon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSkillIcon()
    {
        if(skillNum == 0)
        {
            skillImage.sprite = null;
            skillImage.color = new Vector4(255, 255, 255, 0);
            return;
        }
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
        if (skillNum == 0)
            return;
        skillUIManager.SetSelectSkill(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(skillUIManager.selectHotKeySlot != null)
        {
            skillUIManager.SetHotKeySkill();
        }
    }
}
