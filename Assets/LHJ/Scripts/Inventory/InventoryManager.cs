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
        
    }
}
