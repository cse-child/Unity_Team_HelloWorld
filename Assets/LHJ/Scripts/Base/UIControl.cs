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
    public GameObject StatusUI;

    private StarterAssetsInputs _input;

    private void Awake()
    {
        InventoryUI = gameObject.transform.Find("Inventory").gameObject;
        QuesteUI = gameObject.transform.Find("Queste").gameObject;
        SkillUI = gameObject.transform.Find("SkillUI").gameObject;
        LootingUI = gameObject.transform.Find("LootingUI").gameObject;
        StatusUI = gameObject.transform.Find("StatusUI").gameObject;
        

        _input = FindObjectOfType<StarterAssetsInputs>();
        ItemDataManager.instance.Awake();
        ItemLootManager.instance.SetLootingUI(LootingUI);
        PlayerInventoryData.instance.SetInventory(InventoryUI.transform.Find("BG").GetComponent<InventoryManager>());
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
                InventoryUI.transform.SetAsLastSibling();
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
                QuesteUI.transform.SetAsLastSibling();
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
                SkillUI.transform.SetAsLastSibling();
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
                LootingUI.transform.SetAsLastSibling();
                LootingUI.SetActive(true);
            }
            CheckCursorState();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(StatusUI.activeSelf)
            {
                StatusUI.SetActive(false);
            }
            else
            {
                StatusUI.transform.SetAsLastSibling();
                StatusUI.SetActive(true);
            }
            CheckCursorState();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log(transform.childCount);
            transform.GetChild(transform.childCount-1).gameObject.SetActive(false);
            transform.GetChild(transform.childCount - 1).transform.SetAsFirstSibling();
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
