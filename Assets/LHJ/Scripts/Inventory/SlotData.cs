using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SlotData : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public int itemNum = 0;
    public int itemCount = 0;
    public Image itemImage;
    public TextMeshProUGUI itemCountText;
    public ItemExplanation itemExplanation;
    public HotKeySlot hotkey = null;
    

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
        //itemCount--;
        //if(itemCount<=0)
        //{
        //    itemNum = 0;
        //    itemCount = 0;
        //    hotkey.SetSlotData(null);
        //    hotkey = null;
        //}    
    }

    public void SubItem(int count)
    {
        if (itemNum == 0)
            return;
        itemCount-= count;
        if (itemCount <= 0)
        {
            itemNum = 0;
            itemCount = 0;
            if (hotkey != null)
            {
                hotkey.SetSlotData(null);
                hotkey = null;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)      
    {
        //������ ���
        if (eventData.button == PointerEventData.InputButton.Right && itemCount > 0)
        {
            UseItem();
        }
    }

    
    public void OnPointerDown(PointerEventData eventData)
    {
        // ������ ������ ������ �Ŵ����� SelectSlot ����
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
        // ���콺 Ŭ���� ������ �̹��� �θ� �ǵ����� ������ ��Ҹ� ��ȯ
        if (itemManager.GetSelectSlotIndex() != -1)
        {
            itemManager.ReturnSlotImage();

            if (itemManager.GetChaingeSlotIndex() != -1)
            {
                itemManager.SwapItemSlot();

            }

            if (itemManager.GetHotKey() != null)
            {
                hotkey = itemManager.GetHotKey();
            }
            if(hotkey != null)
                hotkey.SetSlotData(this);

        }


        itemManager.SetSelectSlot(null, null);
        itemImage.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //��ġ�� �ٲ� ������ ����
        if (itemManager.GetSelectSlotIndex() != -1 && itemManager.GetSlot(itemManager.GetSelectSlotIndex()) != gameObject)
        {
            itemManager.SetChaingeSlot(this.gameObject);
        }

        if (itemNum == 0)
            return;


        //������ ����â Ȱ��ȭ
        itemExplanation.gameObject.SetActive(true);
        itemExplanation.SetItemData(itemNum);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //������ ����â ��Ȱ��ȭ
        itemExplanation.gameObject.SetActive(false);
    }

}
