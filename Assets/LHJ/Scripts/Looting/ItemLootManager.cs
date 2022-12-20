using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLootManager : MonoBehaviour
{
    private static ItemLootManager _instance;
    public static ItemLootManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("ItemLootManager");
                _instance = obj.AddComponent<ItemLootManager>();
            }
            return _instance;
        }
    }


    public LootingUIControl lootingUI;

    List<Dictionary<int, int>> lootItem = new List<Dictionary<int,int>>();


    public void Start()
    {
        SetLootingUI(GameObject.Find("LootingUI"));
    }

    public void ClearLootItem()
    {
        lootItem.Clear();
    }

    public void AddLootItem(int itemNum, int count)
    {
        Dictionary<int, int> temp = new Dictionary<int, int>();
        temp.Add(itemNum, count);
        lootItem.Add(temp);
    }

    public void OpenLootingUI()
    {
        
    }

    public void SetLootingUI(GameObject UI)
    {
        lootingUI = UI.GetComponent<LootingUIControl>();
    }


}
