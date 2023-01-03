using StarterAssets;
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


    public LootingUIControl lootingUIControl;
    public GameObject lootingUI;

    List<Dictionary<int, int>> lootItem = new List<Dictionary<int,int>>();


    public void Start()
    {
    }

    public void ClearLootItem()
    {
        lootItem.Clear();
    }

    public void AddLootItem(int itemNum, int count)     //드랍아이템 추가
    {
        foreach(Dictionary<int,int> searcher in lootItem)
        {
            foreach(KeyValuePair<int,int> s in searcher)
            {
                if(s.Key == itemNum)
                {
                    searcher[itemNum] += count;
                    return;
                }
            }
        }
        Dictionary<int, int> temp = new Dictionary<int, int>();
        temp.Add(itemNum, count);
        lootItem.Add(temp);
    }

    public void OpenLootingUI()         //아이템 드랍창 키기
    {
        lootingUIControl.SetLootItem(lootItem);
        lootingUI.transform.SetAsLastSibling();
        lootingUI.SetActive(true);
        FindObjectOfType<StarterAssetsInputs>().SetCursorLocked(false);
        FindObjectOfType<StarterAssetsInputs>().cursorLocked = false;
    }

    public void SetLootingUI(GameObject UI)
    {
        lootingUI = UI;
        lootingUIControl = UI.transform.Find("ItemGrid").GetComponent<LootingUIControl>();
    }


}
