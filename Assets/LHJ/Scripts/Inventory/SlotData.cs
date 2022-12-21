using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SlotData : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public int itemNum = 0;
    public int itemCount = 0;
    public Image itemImage;
    public TextMeshProUGUI itemCountText;
    public ItemExplanation itemExplanation;

    Vector2 slotOffset = new Vector2(50.0f,50.0f);

    private InventoryManager itemManager;

    // Start is called before the first frame update
    void Start()
    {
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
        itemCountText = transform.Find("ItemCount").GetComponent<TextMeshProUGUI>();
        itemExplanation = transform.parent.parent.Find("ItemExplanation").GetComponent<ItemExplanation>();
        itemManager = transform.parent.GetComponent<InventoryManager>();
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

    public void OnPointerEnter(PointerEventData eventData) // 아이템 설명창
    {
        if (itemNum == 0)
            return;


        itemExplanation.gameObject.SetActive(true);
        itemExplanation.SetItemData(itemNum);
    }


    /*
     * 22.12.20 아이템 슬롯 교환 잠정적 보류 LHJ
    private int selectIndex = -1;
    private int chaingeIndex = -1;
    private GameObject itemImage = null;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && itemCount > 0)
        {
            if (itemManager.GetSelectSlotIndex() == -1)
            {
                itemManager.SetSelectSlot(gameObject, transform.Find("ItemImage").gameObject);
            }
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (itemManager.GetSelectSlotIndex() != -1 && itemManager.GetChaingeSlotIndex() != -1)
            itemManager.SwapItemSlot();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemManager.GetSelectSlotIndex() != -1 && itemManager.GetSlot(itemManager.GetSelectSlotIndex()) != gameObject)
        {
            itemManager.SetChaingeSlot(this.gameObject);
            Debug.Log("chaingeIndex : " + gameObject.name);
        }

        if (itemNum == 0)
            return;


        itemExplanation.gameObject.SetActive(true);
        itemExplanation.SetItemData(itemNum);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemExplanation.gameObject.SetActive(false);
    }
    */
}
