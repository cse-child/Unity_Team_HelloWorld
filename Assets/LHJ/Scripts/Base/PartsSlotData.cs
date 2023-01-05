using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PartsSlotData : MonoBehaviour,IPointerDownHandler
{
    public string slotParts;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (slotParts == "weapon")
                PlayerEquipmentManager.instance.WearWepon(0, 0);
            else
                PlayerEquipmentManager.instance.WearArmor(slotParts, 0, 0);
        }
    }

    public void SetSlotParts(string slot)
    {
        slotParts = slot;
    }
}
