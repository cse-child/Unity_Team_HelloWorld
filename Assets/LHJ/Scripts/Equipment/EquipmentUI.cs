using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    private Dictionary<string, GameObject> slots = new Dictionary<string, GameObject>();

    private void Awake()
    {
        PlayerEquipmentManager.instance.SetEquipmentSlotObject(this);
        slots["top"] = transform.Find("HadSlot").Find("ItemImage").gameObject;
        slots["hat"] = transform.Find("TopSlot").Find("ItemImage").gameObject;
        slots["shoes"] = transform.Find("PantsSlot").Find("ItemImage").gameObject;
        slots["pants"] = transform.Find("ShoesSlot").Find("ItemImage").gameObject;
        slots["weapon"] = transform.Find("WeaponSlot").Find("ItemImage").gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {

        //state = transform.Find("State").Find("Itemimage").gameObject;
    }

    public void OnEnable()
    {
        SetSlotImage();
    }

    public void SetSlotImage()      //���Թ�ȣ�� �´� �̹��� ����
    {
        foreach(KeyValuePair<string,GameObject> slot in slots)
        {
            if (PlayerEquipmentManager.instance.GetSlotItemNum(slot.Key) == 0)
            {
                slot.Value.GetComponent<Image>().color = new Vector4(255, 255, 255, 0);
                continue;
            }
            slot.Value.GetComponent<Image>().sprite = ItemDataManager.instance.GetItemData(PlayerEquipmentManager.instance.GetSlotItemNum(slot.Key)).image;
            slot.Value.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
