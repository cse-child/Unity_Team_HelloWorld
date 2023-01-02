using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;
using UnityEngine.Windows;

public class LootingUIControl : MonoBehaviour
{

    List<Dictionary<int, int>> lootItem = new List<Dictionary<int, int>>();
    List<GameObject> slots = new List<GameObject>();
    public GameObject dropItemPrefab;

    private void Awake()
    {
    }

    public void OnEnable()
    {
        OpenLootingUI();
    }


    public void OpenLootingUI()         // ����â ������ �������� �������� �̿��� ������Ʈ�� ����
    {
        for(int i =0; i<lootItem.Count;i++)
        {
            GameObject slot = Instantiate(dropItemPrefab);
            slot.transform.parent = this.gameObject.transform;
            slot.transform.localScale = new Vector3(1, 1, 1);
            foreach (KeyValuePair<int, int> data in lootItem[i])
            {
                slot.transform.Find("ItemImage").GetComponent<Image>().sprite = ItemDataManager.instance.GetItemData(data.Key).image;
                slot.transform.Find("ItemCount").GetComponent<Text>().text = data.Value.ToString();
                slot.transform.Find("ItemInfo").GetComponent<Text>().text = ItemDataManager.instance.GetItemData(data.Key).name;
            }
            slots.Add(slot);
        }
    }

    public void LootItem()          // ������ ����ݱ� ���� ������ ��ü ȹ��
    {
        for (int i = 0; i < lootItem.Count; i++)
        {
            foreach (KeyValuePair<int, int> data in lootItem[i])
            {
                PlayerInventoryData.instance.AddItem(data.Key, data.Value);
            }
        }
        for(int i=0;i < slots.Count;i++)
        {
            Destroy(slots[i]);
        }
        slots.Clear();
        lootItem.Clear();
        ItemLootManager.instance.ClearLootItem();
        FindObjectOfType<StarterAssetsInputs>().SetCursorLocked(true);
        FindObjectOfType<StarterAssetsInputs>().cursorLocked = true;
    }

    public void SetLootItem(List<Dictionary<int,int>> lootItem) 
    {
        this.lootItem = lootItem;
    }
}
