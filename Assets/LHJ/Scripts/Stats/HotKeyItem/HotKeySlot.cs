using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HotKeySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private int itemNum = 0;
    private int itemCount = 0;
    private Image itemImage;
    private TextMeshProUGUI itemCountText;
    private SlotData slotData;
    private InventoryManager inventoryManager;
    private KeyCode commend;

    // Start is called before the first frame update
    void Start()
    {
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
        itemCountText = transform.Find("ItemCount").GetComponent<TextMeshProUGUI>();
        inventoryManager = PlayerInventoryData.instance.inventory;
    }

    // Update is called once per frame
    void Update()
    {
        Draw();

       
        UseItem();
    }

    public void Draw()
    {
        if (slotData != null)
        {
            itemNum = slotData.itemNum;
            itemCount = slotData.itemCount;
        }

        if (itemNum == 0 || slotData == null)
        {
            itemImage.sprite = null;
            itemImage.color = new Vector4(255, 255, 255, 0);
            itemCountText.text = "";
        }
        else
        {
            itemImage.sprite = ItemDataManager.instance.GetItemData(itemNum).image;
            itemImage.color = new Vector4(255, 255, 255, 255);
            itemCountText.text = itemCount.ToString();
        }
    }

    public void UseItem()
    {
        if (Input.GetKeyDown(commend) && slotData != null)
        {
            slotData.UseItem();
        }
    }

    public void SetSlotData(SlotData slot)
    {
        slotData = slot;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryManager.SetInHotKey(true, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryManager.SetInHotKey(false);
    }

    public void SetCommend(KeyCode key)
    {
        commend = key;
    }
}
