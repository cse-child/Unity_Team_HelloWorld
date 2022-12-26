using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
            ItemLootManager.instance.AddLootItem(1, 3);
        if(Input.GetKeyDown(KeyCode.A))
            ItemLootManager.instance.AddLootItem(2, 3);

        if (Input.GetKeyDown(KeyCode.F))
            ItemLootManager.instance.OpenLootingUI();
    }
}
