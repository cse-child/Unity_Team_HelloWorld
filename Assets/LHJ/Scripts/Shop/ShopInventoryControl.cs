using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopInventoryControl : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public GameObject ShopItemPrefab;

    public Dictionary<int,int> hasItem = new Dictionary<int,int>();
    public List<GameObject> slots = new List<GameObject> ();
    public TextMeshProUGUI hasGoldText;
    bool isEnter = false;

    public void OnEnable()
    {
        hasItem = PlayerInventoryData.instance.GetHasInventory();
        if(hasItem.Count > slots.Count)
        {
            int addCount = hasItem.Count - slots.Count;
            for(int i =0; i< addCount; i++)
            {
                GameObject temp = Instantiate(ShopItemPrefab);
                temp.transform.parent = this.transform;
                temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                slots.Add(temp);
            }
        }

        int index = 0;
        foreach(KeyValuePair<int,int> item in hasItem)
        {
            slots[index].GetComponent<ShopInventorySlotData>().SetSlot(item.Key, item.Value);
            slots[index].SetActive(true);
            index++;
        }
    }

    public void SetSlotUpdate(GameObject data)
    {
        foreach(GameObject slot in slots)
        {
            if(slot == data)
                slot.SetActive(false);
        }
    }

    public void OnDisable()
    {
        foreach(GameObject slot in slots)
        {
            slot.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hasGoldText.text = PlayerInventoryData.instance.GetPlayerState().gold.ToString();

        if (isEnter)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                foreach (GameObject slot in slots)
                    slot.transform.position += new Vector3(0, 10, 0);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                //if (slots[0].GetComponent<RectTransform>().localPosition.y >= -80)
                //{
                foreach (GameObject slot in slots)
                    slot.transform.position += new Vector3(0, -10, 0);
                //}
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
    }
}
