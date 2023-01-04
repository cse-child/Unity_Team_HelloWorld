using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotData : MonoBehaviour
{
    private Text itemNameText;
    private Text itemPriceText;
    private Image itemImage;

    public ShopInventoryControl inventoryControl;

    private int itemNum = 0;
    private int itemCount = 0;
    private int itemPrice = 0;
    private string itemName = "";
    private Sprite itemSprite;
    private bool isClick = false;
    private float clickTime = 0.0f;
    private float dobleClickTime = 0.5f;

    public void Awake()
    {
        itemNameText = transform.Find("NameFrame").Find("ItemName").GetComponent<Text>();
        itemPriceText = transform.Find("NameFrame").Find("ItemPrice").GetComponent<Text>();
        itemImage = transform.Find("Border").Find("ItemImage").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        itemNameText.text = itemName;
        itemPriceText.text = itemPrice.ToString();
        itemImage.sprite = itemSprite;

        if (isClick == true)
        {
            clickTime += Time.deltaTime;
            if (clickTime > dobleClickTime)
            {
                isClick = false;
                clickTime = 0.0f;
            }
        }
    }

    public void SetinventoryControl(  ShopInventoryControl inventoryControl)
    {
        this.inventoryControl = inventoryControl;
    }

    public void SetSlot(int itemNum, int itemCount)
    {
        this.itemNum = itemNum;
        this.itemCount = itemCount;
        this.itemPrice = ItemDataManager.instance.GetItemData(itemNum).price;
        itemSprite = ItemDataManager.instance.GetItemData(itemNum).image;
        itemName = ItemDataManager.instance.GetItemData(itemNum).name;
        itemImage.color = new Vector4(255, 255, 255, 255);
    }

    public void isBuy()
    {
        if (isClick == true)    //더블클릭
        {
            if (PlayerInventoryData.instance.GetPlayerState().gold >= itemPrice)
            {
                PlayerInventoryData.instance.GetPlayerState().gold -= itemPrice;
                PlayerInventoryData.instance.AddItem(itemNum, 1);
                inventoryControl.OnEnable();
            }
        }
        else
            isClick = true;
    }
}
