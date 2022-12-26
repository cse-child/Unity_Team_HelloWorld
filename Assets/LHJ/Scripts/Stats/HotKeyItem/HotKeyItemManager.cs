using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotKeyItemManager : MonoBehaviour
{
    public GameObject hotkeySlot;
    private List<GameObject> slots = new List<GameObject>();
    Vector3 startPos = new Vector3(80.0f, -605.0f, 0);
    Vector3 offsetX = new Vector3(100.0f, 0, 0);
    Vector3 offsetY = new Vector3(0, -100.0f, 0);
    int keyNum = 1;


    void Start()
    {
        for(int i =0; i<2;i++)
        {
            for(int j=0;j<4;j++)
            {
                GameObject temp = Instantiate(hotkeySlot);
                temp.name = "HotKeySlot" + keyNum;
                temp.transform.parent = this.transform;
                temp.transform.localScale = new Vector3(1, 1, 1);
                temp.transform.localPosition = startPos + (offsetX * j) + (offsetY * i);
                temp.transform.Find("KeyName").GetComponent<TextMeshProUGUI>().text = (keyNum).ToString();
                temp.GetComponent<HotKeySlot>().SetCommend((KeyCode)(48 +keyNum));
                slots.Add(temp);
                keyNum++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
