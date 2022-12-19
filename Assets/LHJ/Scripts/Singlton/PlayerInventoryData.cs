using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryData : MonoBehaviour
{
    private static PlayerInventoryData _instance;
    public static PlayerInventoryData instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject obj = new GameObject("PlayerInventoryData");
                _instance = obj.AddComponent<PlayerInventoryData>();
            }
            return _instance;
        }
    }

    /*=====================================변수===============================*/
    private Dictionary<int, int> hasItems = new Dictionary<int, int>();

    public InventoryManager inventory;

    /*========================================================================*/

    private void Awake()
    {
        ItemDataManager.instance.Awake();
    }

    void Start()
    {
        AddItem(1, 3);
        AddItem(2, 2);
        AddItem(1, 2);
    }

    //플레이어소유 아이템 추가
    public void AddItem(int itemNum, int itemCount)
    {
        if(hasItems.ContainsKey(itemNum))
        {
            hasItems[itemNum] += itemCount;
        }
        else
        {
            hasItems[itemNum] = itemCount;
        }
        inventory.SetItemSlot(itemNum, hasItems[itemNum]);
    }

    public void SubItem(int itemNum, int removeCount)
    {
        hasItems[itemNum] -= removeCount;
        if (hasItems[itemNum] <= 0)
            hasItems.Remove(itemNum);
    }

    public void SetInventory(InventoryManager inventory)
    {
        this.inventory = inventory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
