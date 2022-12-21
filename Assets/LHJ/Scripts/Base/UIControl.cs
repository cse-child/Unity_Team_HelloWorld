using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject InventoryUI;
    public GameObject QuesteUI;
    public GameObject SkillUI;
    public GameObject LootingUI;

    private StarterAssetsInputs _input;

    private void Awake()
    {
        InventoryUI = gameObject.transform.Find("Inventory").gameObject;
        QuesteUI = gameObject.transform.Find("Queste").gameObject;
        SkillUI = gameObject.transform.Find("SkillUI").gameObject;
        LootingUI = gameObject.transform.Find("LootingUI").gameObject;
        
        _input = FindObjectOfType<StarterAssetsInputs>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(InventoryUI.activeSelf)
            {
                InventoryUI.SetActive(false);
            }
            else
            {
                InventoryUI.SetActive(true);
            }
            CheckCursorState();
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
            CheckCursorState();
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
            CheckCursorState();
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
            CheckCursorState();
        }
    }

    private void CheckCursorState()
    {
        if (InventoryUI.activeSelf || QuesteUI.activeSelf || SkillUI.activeSelf || LootingUI.activeSelf)
            _input.SetCursorLocked(false);
        else
            _input.SetCursorLocked(true);
    }
}
