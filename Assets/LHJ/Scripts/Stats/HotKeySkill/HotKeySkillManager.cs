using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        SetSlotData(0, "Q", KeyCode.Q);
        SetSlotData(1, "W", KeyCode.W);
        SetSlotData(2, "E", KeyCode.E);
        SetSlotData(3, "R", KeyCode.R);
        SetSlotData(4, "A", KeyCode.A);
        SetSlotData(5, "S", KeyCode.S);
        SetSlotData(6, "D", KeyCode.D);
        SetSlotData(7, "F", KeyCode.F);

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
