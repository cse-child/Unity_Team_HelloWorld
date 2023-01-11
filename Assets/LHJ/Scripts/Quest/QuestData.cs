using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class QuestData : MonoBehaviour, IPointerClickHandler
{
    private QuestUIControl uiControl;
    private Text questName;
    private QuestDataManager.QuestData questData;
    private Dictionary<int, int> achivementItem = new Dictionary<int, int>();

    public int achivementExp = 0;

    public void Start()
    {
        questName = transform.Find("Text").GetComponent<Text>();
    }

    public void Update()
    {
        questName.text = questData.name;
    }

    public void SetQuestData(QuestDataManager.QuestData data)
    {
        questData = data;
        string[] row = questData.achivement.Split("\\");
        achivementExp = int.Parse(row[0].Substring(row[0].IndexOf("_") + 1));
        for (int i = 1; i < row.Length; i++)        //퀘스트 보상 첫번째는 반드시 EXP가 들어가므로 1부터 검사
        {
            int itemNum;
            int itemCount;
            itemNum = int.Parse(row[i].Substring(0, row[i].IndexOf("_")));
            itemCount = int.Parse(row[i].Substring(row[i].IndexOf("_") + 1));
            achivementItem[itemNum] = itemCount;
        }
    }
    
    public QuestDataManager.QuestData GetQuestData()
    {
        return questData;
    }

    public void SetUiControlor(QuestUIControl control)
    {
        uiControl = control;
    }

    public void OnPointerClick(PointerEventData eventData)      //선택퀘스트 변경및 기존 퀘스트 보상등 제거
    {
        uiControl.SetSelectQuest(this.gameObject);
        uiControl.ClearAchivement();
        SetQuestAchivement();
    }

    public void SetQuestAchivement()            // 퀘스트보상 설정
    {
        string[] row = questData.achivement.Split("\\");
        for (int i =1; i<row.Length;i++)        //퀘스트 보상 첫번째는 반드시 EXP가 들어가므로 1부터 검사
        {
            int itemNum;
            int itemCount;
            itemNum = int.Parse(row[i].Substring(0, row[i].IndexOf("_")));
            itemCount = int.Parse(row[i].Substring(row[i].IndexOf("_")+1));
            uiControl.SetAchivement(ItemDataManager.instance.GetItemData(itemNum).image, itemCount);
        }
    }

    public int GetAchivementExp()
    {
        return achivementExp;
    }

    public void ClearAchivementItem()
    {
        achivementItem.Clear();
    }

    public Dictionary<int,int> GetAchivementItem()
    {
        return achivementItem;
    }
}
