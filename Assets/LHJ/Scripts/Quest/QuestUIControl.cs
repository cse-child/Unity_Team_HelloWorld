using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUIControl : MonoBehaviour
{
    public GameObject questNamePrefab;

    private List<GameObject> hasQuests = new List<GameObject>();
    private GameObject questList;
    private Text questInfo;
    private Text target;
    private Text achivementExp;
    private List<GameObject> achivementObject = new List<GameObject>();
    private GameObject selectQuest;
    private GameObject selectQuestbar;

    private void Awake()
    {
        QuestDataManager.instance.SetQuestUIControl(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        questList = transform.Find("QuestList").gameObject;
        selectQuestbar = transform.Find("SelectQuest").gameObject;

        GameObject questInfoObject = transform.Find("QuestInfo").gameObject;

        questInfo = questInfoObject.transform.Find("Info").GetComponent<Text>();
        target = questInfoObject.transform.Find("Target").GetComponent<Text>();
        achivementExp = questInfoObject.transform.Find("EXP").Find("EXPPluse").GetComponent<Text>();
        for (int i = 1; i < 6; i++)
        {
            achivementObject.Add(questInfoObject.transform.Find("ItemTable").transform.Find("Achivement"+i).gameObject);
        }
     
        AddQuest("qst_005");
    }

    // Update is called once per frame
    void Update()
    {
        if (selectQuest != null)
        {
            selectQuestbar.transform.position = selectQuest.transform.position;
            string temp = selectQuest.GetComponent<QuestData>().GetQuestData().detail.Replace("ee","\n");
            questInfo.text = temp;
            target.text = selectQuest.GetComponent<QuestData>().GetQuestData().goal;
            achivementExp.text = selectQuest.GetComponent<QuestData>().GetAchivementExp().ToString();
        }
    }

    public void AddQuest(string questNum)
    {
        GameObject temp = Instantiate(questNamePrefab);
        temp.transform.parent = questList.transform;
        temp.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        temp.GetComponent<QuestData>().SetQuestData(QuestDataManager.instance.GetQuest(questNum));
        temp.GetComponent<QuestData>().SetUiControlor(this);
        hasQuests.Add(temp);
        if (hasQuests.Count == 1)
        {
            selectQuestbar.SetActive(true);
            SetSelectQuest(hasQuests[0]);
        }
    }

    public void SetSelectQuest(GameObject selectQuest)
    {
        this.selectQuest = selectQuest;
    }

    public void ClearAchivement()
    {
        foreach (GameObject temp in achivementObject)
        {
            temp.transform.Find("Item").GetComponent<Image>().sprite = null;
            temp.transform.Find("Count").GetComponent<TextMeshProUGUI>().text = "";
            temp.SetActive(false);
        }
    }

    public void SetAchivement(Sprite itemImage, int Count)
    {
        foreach (GameObject temp in achivementObject)
        {
            Image tempImage = temp.transform.Find("Item").GetComponent<Image>();
            if (tempImage.sprite != null)
                continue;
            tempImage.sprite = itemImage;
            if(Count > 1)
                temp.transform.Find("Count").GetComponent<TextMeshProUGUI>().text = Count.ToString();

            if (tempImage.sprite != null)
                temp.SetActive(true);
            break;
        }
    }

    public void ClearQuest(string questNum)
    {
        for(int i=0; i<hasQuests.Count;i++)
        {
            if(questNum == hasQuests[i].GetComponent<QuestData>().GetQuestData().questNum)
            {
                foreach (KeyValuePair<int, int> item in hasQuests[i].GetComponent<QuestData>().GetAchivementItem())
                {
                    PlayerInventoryData.instance.AddItem(item.Key, item.Value);
                }
                hasQuests[i].GetComponent<QuestData>().ClearAchivementItem();
                hasQuests[i].SetActive(false);
                hasQuests.Remove(hasQuests[i]);
                break;
            }
        }
    }
}
