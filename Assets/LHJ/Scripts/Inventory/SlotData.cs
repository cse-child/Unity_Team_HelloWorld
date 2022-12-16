using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SlotData : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int itemNum = 0;
    public int itemCount = 0;
    public Image itemImage;
    public TextMeshProUGUI itemCountText;
    public ItemExplanation itemExplanation;

    // Start is called before the first frame update
    void Start()
    {
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
        itemCountText = transform.Find("ItemCount").GetComponent<TextMeshProUGUI>();
        itemExplanation = transform.parent.parent.Find("ItemExplanation").GetComponent<ItemExplanation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (itemNum == 0)
        {
            itemImage.sprite = null;
            itemImage.color = new Vector4(255, 255, 255, 0);
        }
        else
        {
            itemImage.sprite = ItemDataManager.instance.GetItemData(itemNum).image;
            itemImage.color = new Vector4(255, 255, 255, 255);
        }
        itemCountText.text = itemCount.ToString();
    }

    public void UseItem()
    {
        PlayerInventoryData.instance.SubItem(itemNum, 1);
        itemCount--;
        if(itemCount<=0)
        {
            itemNum = 0;
            itemCount = 0;
        }    
    }

    public void OnPointerClick(PointerEventData eventData)      //아이템 사용
    {
        if (eventData.button == PointerEventData.InputButton.Right && itemCount > 0)
        {
            UseItem();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemNum == 0)
            return;
        //itemExplanation.transform.position = Input.mousePosition;

        itemExplanation.gameObject.SetActive(true);
        itemExplanation.SetItemData(itemNum);
       // throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemExplanation.gameObject.SetActive(false);
    }
}
