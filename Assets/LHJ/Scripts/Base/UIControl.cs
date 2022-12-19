using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject InventoryUI;
    public GameObject QuesteUI;
    public GameObject SkillUI;
    public GameObject LootingUI;


    private void Awake()
    {
        InventoryUI = gameObject.transform.Find("Inventory").gameObject;
        QuesteUI = gameObject.transform.Find("Queste").gameObject;
        SkillUI = gameObject.transform.Find("SkillUI").gameObject;
        LootingUI = gameObject.transform.Find("LootingUI").gameObject;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(InventoryUI.activeSelf)
            {
                InventoryUI.SetActive(false);
            }
            else
            {
                InventoryUI.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            if(QuesteUI.activeSelf)
            {
                QuesteUI.SetActive(false);
            }
            else
            {
                QuesteUI.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(SkillUI.activeSelf)
            {
                SkillUI.SetActive(false);
            }
            else
            {
                SkillUI.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            if(LootingUI.activeSelf)
            {
                LootingUI.SetActive(false);
            }
            else
            {
                LootingUI.SetActive(true);
            }
        }
    }

}
