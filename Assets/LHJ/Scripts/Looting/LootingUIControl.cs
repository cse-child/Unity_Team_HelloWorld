using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootingUIControl : MonoBehaviour
{
    private void Awake()
    {
        ItemLootManager.instance.SetLootingUI(this.gameObject);  // UIControlor�� �̵��ʿ�
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
