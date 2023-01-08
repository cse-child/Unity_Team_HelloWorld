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
    public GameObject ShopUI;
    public PlayerState playerState;

    private StarterAssetsInputs _input;

    private void Awake()
    {
        InventoryUI = gameObject.transform.Find("Inventory").gameObject;
        QuesteUI = gameObject.transform.Find("Queste").gameObject;
        SkillUI = gameObject.transform.Find("SkillUI").gameObject;
        LootingUI = gameObject.transform.Find("LootingUI").gameObject;
        StatusUI = gameObject.transform.Find("StatusUI").gameObject;
        ShopUI = gameObject.transform.Find("ShopUI").gameObject;
        playerState = FindObjectOfType<PlayerState>();
        

        _input = FindObjectOfType<StarterAssetsInputs>();
        ItemLootManager.instance.SetLootingUI(LootingUI);
        PlayerInventoryData.instance.SetInventory(InventoryUI.transform.Find("BG").GetComponent<InventoryManager>());
        PlayerInventoryData.instance.SetPlayerState(playerState);
        ItemDataManager.instance.Non();
        PlayerEquipmentManager.instance.Non();
        MonsterUIManager.instance.Non();
    }

    public void Start()
    {
        StatusUI.SetActive(true);
        StatusUI.SetActive(false);

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
            CheckCursorState();
        }
    }

    public void CheckCursorState()
    {
        if (InventoryUI.activeSelf || QuesteUI.activeSelf || SkillUI.activeSelf || LootingUI.activeSelf)
        { 
            _input.SetCursorLocked(false);
            _input.cursorInputForLook = false;
        }
        else
        {
            _input.SetCursorLocked(true);
            _input.cursorInputForLook = true;
        }
    }
}
