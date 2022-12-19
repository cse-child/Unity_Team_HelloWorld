using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    private List<GameObject> slots = new List<GameObject>();


    private int selectIndex = -1;
    private GameObject itemImage = null;

    private void Awake()
    {
        PlayerInventoryData.instance.SetInventory(this); 
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject temp = Instantiate(slotPrefab);
            temp.transform.SetParent(transform);
            temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
           
            slots.Add(temp);
        }
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


    // Update is called once per frame
    void Update()
    {

        if (selectIndex != -1)
        {
            itemImage.transform.position = Input.mousePosition;
        }


    }

    public void SetSelectSlot(GameObject slot, GameObject item)
    {
       for(int i =0; i<slots.Count;i++)
        {
            if (slots[i] == slot)
            {
                selectIndex = i;
                itemImage = item;
            }
       }
    }

    public void SwapItemSlot(GameObject target)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == target)
            {
                GameObject temp;
                temp = slots[i];
                slots[i] = slots[selectIndex];
                slots[selectIndex] = temp;
                selectIndex = -1;
                itemImage = null;
                slots[selectIndex].transform.Find("ItemImage").GetComponent<RectTransform>().position = slots[selectIndex].transform.position;
                slots[i].transform.Find("ItemImage").GetComponent<RectTransform>().position = slots[i].transform.position;
            }
        }
    }

    public int GetSelectSlotIndex()
    {
        return selectIndex;
    }
}
