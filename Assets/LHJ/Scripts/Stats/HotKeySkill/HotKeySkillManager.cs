using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class HotKeySkillManager : MonoBehaviour
{
    public GameObject hotkeySlot;
    private List<GameObject> slots = new List<GameObject>();
    
    Vector3 startPos = new Vector3(-510.0f, -335.0f, 0);
    Vector3 offsetX = new Vector3(100.0f, 0, 0);
    Vector3 offsetY = new Vector3(0, -100.0f, 0);
    int keyNum = 1;


    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject temp = Instantiate(hotkeySlot);
                //temp.name = "HotKeySlot" + keyNum;
                temp.transform.parent = this.transform;
                temp.transform.localScale = new Vector3(1, 1, 1);
                temp.transform.localPosition = startPos + (offsetX * j) + (offsetY * i);
                slots.Add(temp);
                keyNum++;
            }
        }

        SetSlotData(0, "F1", KeyCode.F1);
        SetSlotData(1, "F2", KeyCode.F2);
        SetSlotData(2, "F3", KeyCode.F3);
        SetSlotData(3, "F4", KeyCode.F4);
        SetSlotData(4, "F5", KeyCode.F5);
        SetSlotData(5, "F6", KeyCode.F6);
        SetSlotData(6, "F7", KeyCode.F7);
        SetSlotData(7, "F8", KeyCode.F8);

    }

    private void SetSlotData(int num, string key, KeyCode code)
    {
        slots[num].name = "HotKeySlot"+ key;
        slots[num].transform.Find("KeyName").GetComponent<TextMeshProUGUI>().text = key;
        slots[num].GetComponent<HotKeySkillSlot>().SetCommend(code);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
