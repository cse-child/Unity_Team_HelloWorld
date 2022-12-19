using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemExplanation : MonoBehaviour
{
    private Image itemIcon;
    private Text itemName;
    private Text itemExplanation;

    ItemDataManager.ItemData itemData;

    // Start is called before the first frame update
    void Start()
    {
        itemIcon = transform.Find("Image").GetComponent<Image>();
        itemName = transform.Find("ItemName").GetComponent<Text>();
        itemExplanation = transform.Find("Explanation").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        itemIcon.sprite = itemData.image;
        itemName.text = itemData.name;
        itemExplanation.text = itemData.explanation.Replace("ee","\n");
    }

    public void SetItemData(int itemNum)
    {
       itemData = ItemDataManager.instance.GetItemData(itemNum);
    }
}
