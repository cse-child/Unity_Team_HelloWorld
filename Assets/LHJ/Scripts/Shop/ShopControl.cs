using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.EventSystems;

public class ShopControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject ShopItemPrefab;
    public List<GameObject> slots = new List<GameObject>();
    public ShopInventoryControl inventoryControl;
    bool isEnter = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i<11;i++)
        {
            GameObject temp = Instantiate(ShopItemPrefab);
            temp.transform.parent = this.transform;
            temp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            temp.GetComponent<ShopSlotData>().SetinventoryControl(inventoryControl);
            slots.Add(temp);
        }
        
        slots[0].GetComponent<ShopSlotData>().SetSlot(1, 1);
        slots[1].GetComponent<ShopSlotData>().SetSlot(2, 1);
        slots[2].GetComponent<ShopSlotData>().SetSlot(3, 1);
        slots[3].GetComponent<ShopSlotData>().SetSlot(4, 1);
        slots[4].GetComponent<ShopSlotData>().SetSlot(5, 1);
        slots[5].GetComponent<ShopSlotData>().SetSlot(6, 1);
        slots[6].GetComponent<ShopSlotData>().SetSlot(7, 1);
        slots[7].GetComponent<ShopSlotData>().SetSlot(10, 1);
        slots[8].GetComponent<ShopSlotData>().SetSlot(13, 1);
        slots[9].GetComponent<ShopSlotData>().SetSlot(16, 1);
        slots[10].GetComponent<ShopSlotData>().SetSlot(19, 1);

        
    }

    // Update is called once per frame
    void Update()
    {
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
