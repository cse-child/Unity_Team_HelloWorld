using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    private List<GameObject> slots = new List<GameObject>();



    private void Awake()
    {
        PlayerInventoryData.instance.SetInventory(this); 
    }

    private void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject temp = Instantiate(slotPrefab);
            temp.name = "slot" + i;
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

    }


    /*
     * 22.12.20 아이템 슬롯 교환 잠정적 보류 LHJ
     * 
    
    private int selectIndex = -1;
    private int chaingeIndex = -1;
    private GameObject itemImage = null;


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
        GameObject temp = slots[selectIndex];
        slots[selectIndex] = slots[chaingeIndex];
        slots[chaingeIndex] = temp;
        Debug.Log("고른 슬롯 : " + slots[selectIndex].name);
        Debug.Log("바뀌는 슬롯 : " + slots[chaingeIndex].name);
        selectIndex = -1;
        chaingeIndex = -1;
    }

    public int GetSelectSlotIndex()
    {
        return selectIndex;
    }
    public int GetChaingeSlotIndex()
    {
        return chaingeIndex;
    }

    public GameObject GetSlot(int index)
    {
        return slots[index];
    }
    */
}
