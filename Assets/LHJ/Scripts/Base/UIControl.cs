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
    public GameObject DeathUI;
    public PlayerState playerState;
    public GameObject questListPrefab;

    private StarterAssetsInputs _input;

    private void Awake()
    {
        InventoryUI = gameObject.transform.Find("Inventory").gameObject;
        QuesteUI = gameObject.transform.Find("Queste").gameObject;
        SkillUI = gameObject.transform.Find("SkillUI").gameObject;
        LootingUI = gameObject.transform.Find("LootingUI").gameObject;
        StatusUI = gameObject.transform.Find("StatusUI").gameObject;
        ShopUI = gameObject.transform.Find("ShopUI").gameObject;
        DeathUI = gameObject.transform.Find("DeathUI").gameObject;
        playerState = FindObjectOfType<PlayerState>();


        _input = FindObjectOfType<StarterAssetsInputs>();
        ItemLootManager.instance.SetLootingUI(LootingUI);
        PlayerInventoryData.instance.SetInventory(InventoryUI.transform.Find("BG").GetComponent<InventoryManager>());
        PlayerInventoryData.instance.SetPlayerState(playerState);
        ItemDataManager.instance.Non();
        PlayerEquipmentManager.instance.Non();
        MonsterUIManager.instance.Non();
        QuestAlarmManager.instance.Non();
        QuestDataManager.instance.SetQuestUIControl(QuesteUI.GetComponent<QuestUIControl>());
        MonsterUIManager.instance.SetActiveMonsterUI(false);
    }

    public void Start()
    {
        InventoryUI.SetActive(false);
        QuesteUI.SetActive(false);
        SkillUI.SetActive(false);
        LootingUI.SetActive(false);
        StatusUI.SetActive(false);
        ShopUI.SetActive(false);
        DeathUI.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryUI.activeSelf)
            {
                InventoryUI.SetActive(false);
            }
            else
            {
                InventoryUI.transform.SetAsLastSibling();
                InventoryUI.SetActive(true);
                UISoundControl.instance.SoundPlay(1);
            }
            CheckCursorState();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (QuesteUI.activeSelf)
            {
                QuesteUI.SetActive(false);
            }
            else
            {
                QuesteUI.transform.SetAsLastSibling();
                QuesteUI.SetActive(true);
                UISoundControl.instance.SoundPlay(1);
            }
            CheckCursorState();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (SkillUI.activeSelf)
            {
                SkillUI.SetActive(false);
            }
            else
            {
                SkillUI.transform.SetAsLastSibling();
                SkillUI.SetActive(true);
                UISoundControl.instance.SoundPlay(1);
            }
            CheckCursorState();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (StatusUI.activeSelf)
            {
                StatusUI.SetActive(false);
            }
            else
            {
                StatusUI.transform.SetAsLastSibling();
                StatusUI.SetActive(true);
                UISoundControl.instance.SoundPlay(1);
            }
            CheckCursorState();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (transform.GetChild(transform.childCount - 1).name == DeathUI.name)
            {
                DeathUI.transform.SetAsFirstSibling();
                transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
                transform.GetChild(transform.childCount - 1).transform.SetAsFirstSibling();
                CheckCursorState();
            }
            else
            {
                transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
                transform.GetChild(transform.childCount - 1).transform.SetAsFirstSibling();
                CheckCursorState();
            }
        }
    }

    public bool CheckCursorState()
    {
        if (InventoryUI.activeSelf || QuesteUI.activeSelf || SkillUI.activeSelf || 
            LootingUI.activeSelf || ShopUI.activeSelf || StatusUI.activeSelf || DeathUI.activeSelf) 
        { 
            _input.SetCursorLocked(false);
            _input.cursorInputForLook = false;
            _input.LookInput(Vector2.zero);
            return false;
        }
        else
        {
            _input.SetCursorLocked(true);
            _input.cursorInputForLook = true;
            return true;
        }
    }

    public void CloseDeathUI()
    {
        DeathUI.SetActive(false);
        CheckCursorState();
    }

    public void OpenDeathUI()
    {
        DeathUI.SetActive(true);
        DeathUI.transform.SetAsFirstSibling();
        CheckCursorState();
    }
}
