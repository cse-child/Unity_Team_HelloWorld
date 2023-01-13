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
    private int hasGold = 0;
    private PlayerState playerState;

    /*========================================================================*/

    private void Awake()
    {
    }

    void Start()
    {
        inventory.CreateSlot();
        AddItem(1, 3);
        AddItem(2, 3);
    }

    //플레이어 소유 아이템 추가
    public void AddItem(int itemNum, int itemCount)
    {
        if (itemNum == 0)
            return;
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
        if (hasItems.ContainsKey(itemNum))
        {
            hasItems[itemNum] -= removeCount;
            inventory.SubItem(itemNum, removeCount);
            if (hasItems[itemNum] <= 0)
                hasItems.Remove(itemNum);
        }
    }

    public int GetItemCount(int itemNum)
    {
        if (!hasItems.ContainsKey(itemNum))
            return 0;
        return hasItems[itemNum];
    }


    public void SetInventory(InventoryManager inventory)
    {
        this.inventory = inventory;
    }

    public Dictionary<int,int> GetHasInventory()
    {
        return hasItems;
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void SetPlayerState(PlayerState playerState)
    {
        this.playerState = playerState;
    }

    public PlayerState GetPlayerState()
    {
        return playerState;
    }
}
