using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IPointerExitHandler
{
    public GameObject slotPrefab;
    private PlayerState playerState;
    private Text goldText;
    private List<GameObject> slots = new List<GameObject>();

    private GameObject tempSlot;

    private int selectIndex = -1;
    private int chaingeIndex = -1;
    private GameObject itemImage = null;

    private HotKeySlot hotKey;
    private bool inHotKey = false;

    private void Awake()
    {
        goldText = transform.parent.Find("Gold").Find("GoldText").GetComponent<Text>();
    }

    public void Start()
    {
        playerState = PlayerInventoryData.instance.GetPlayerState();
    }

    public void CreateSlot()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject temp = Instantiate(slotPrefab);
            temp.name = "slot" + i;
            temp.transform.SetParent(transform);
            temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            slots.Add(temp);
        }
        tempSlot = slots[0].transform.parent.Find("Temp").gameObject;
        tempSlot.transform.SetAsLastSibling();
    }

    //아이템 슬롯 데이터 설정 함수
    public void SetItemSlot(int itemNum, int itemCount)
    {
        foreach(GameObject data in slots)
        {
            SlotData temp = data.GetComponent<SlotData>();
            if(temp.itemNum == itemNum)
            {
                temp.itemCount = itemCount;
                return;
            }
        }
        foreach(GameObject data in slots)
        {
            SlotData temp = data.GetComponent<SlotData>();
            if(temp.itemNum==0)
            {
                temp.itemNum = itemNum;
                temp.itemCount = itemCount;
                return;
            }
        }
    }

    public void SubItem(int itemNum, int count)
    {
        foreach (GameObject data in slots)
        {
            SlotData temp = data.GetComponent<SlotData>();
            if (temp.itemNum == itemNum)
            {
                temp.SubItem(count);
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selectIndex != -1)
        {
            itemImage.transform.position = Input.mousePosition;
        }
        goldText.text = playerState.gold.ToString();
    }

    public void SetSelectSlot(GameObject slot, GameObject item)
    {
        if(slot == null)
        {
            selectIndex = -1;
            return;
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == slot)
            {
                selectIndex = i;
                itemImage = item;

                itemImage.transform.parent = tempSlot.transform;
            }
        }
    }

    public void SetChaingeSlot(GameObject slot)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == slot)
            {
                chaingeIndex = i;
            }
        }
    }

    public void SwapItemSlot()
    {
        //아이템 정보 교환
        SlotData tempSlot = new SlotData();
        SlotData selectSlot = slots[selectIndex].GetComponent<SlotData>();
        SlotData chaingeSlot = slots[chaingeIndex].GetComponent<SlotData>();

        tempSlot.itemNum = chaingeSlot.itemNum;
        tempSlot.itemCount = chaingeSlot.itemCount;
        tempSlot.hotkey = chaingeSlot.hotkey;


        chaingeSlot.itemNum = selectSlot.itemNum;
        chaingeSlot.itemCount = selectSlot.itemCount;
        chaingeSlot.hotkey = selectSlot.hotkey;

        selectSlot.itemNum = tempSlot.itemNum;
        selectSlot.itemCount = tempSlot.itemCount;
        selectSlot.hotkey = tempSlot.hotkey;


        //교환된 위치의 아이템의 핫키설정 유지
        if (chaingeSlot.hotkey != null)
        {
            chaingeSlot.hotkey.SetSlotData(null);
            chaingeSlot.hotkey.SetSlotData(chaingeSlot);
        }
        if (selectSlot.hotkey != null)
        {
            selectSlot.hotkey.SetSlotData(null);
            selectSlot.hotkey.SetSlotData(selectSlot);
        }

        UISoundControl.instance.SoundPlay(2);

        selectIndex = -1;
        chaingeIndex = -1;
    }

    public void ReturnSlotImage()
    {
        itemImage.transform.parent = slots[selectIndex].transform;
    }

    public int GetSelectSlotIndex()
    {
        return selectIndex;
    }
    public int GetChaingeSlotIndex()
    {
        return chaingeIndex;
    }

    public GameObject GetSelectSlot()
    {
        return slots[selectIndex];
    }

    public GameObject GetSlot(int index)
    {
        return slots[index];
    }

    public void SetInHotKey(bool inHotKey, HotKeySlot hotkey = null)
    {
        this.inHotKey = inHotKey;
        this.hotKey = hotkey;
    }

    public HotKeySlot GetHotKey()
    {
        return hotKey;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        chaingeIndex = -1;
    }
}
