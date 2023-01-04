using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    private static PlayerEquipmentManager _instance;
    public static PlayerEquipmentManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("PlayerEquipmentManager");
                _instance = obj.AddComponent<PlayerEquipmentManager>();
            }
            return _instance;
        }
    }

    private Dictionary<string, int> equipmentSlot = new Dictionary<string, int>();
    private EquipmentUI equipmentControl;
    private PlayerState playerState;
    public PlayerPartsControl partsControl;

    private void Awake()
    {
        equipmentSlot.Add("top", 0);
        equipmentSlot.Add("hat", 0);
        equipmentSlot.Add("shoes", 0);
        equipmentSlot.Add("pants", 0);
        equipmentSlot.Add("weapon", 0);
        playerState = FindObjectOfType<PlayerState>();
        partsControl = FindObjectOfType<PlayerPartsControl>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WearWepon(int weponNum, int itemNum)    //��������
    {
        if (equipmentSlot["weapon"] != 0)
        {
            PlayerInventoryData.instance.AddItem(equipmentSlot["weapon"], 1);
            playerState.curAtk = playerState.curAtk - ItemDataManager.instance.GetItemData(equipmentSlot["weapon"]).power;
            partsControl.UnEquippedWeapon();
        }
        equipmentSlot["weapon"] = itemNum;
        equipmentControl.SetSlotImage();
        playerState.curAtk = playerState.baseAtk + ItemDataManager.instance.GetItemData(itemNum).power;
        partsControl.EquippedWeapon(weponNum.ToString());
    }

    public void WearArmor(string itemSlot, int armorNum, int itemNum)       //�� ����
    {

        if (equipmentSlot[itemSlot] != 0)
        {
            PlayerInventoryData.instance.AddItem(equipmentSlot[itemSlot], 1);
            playerState.curDef = playerState.curDef - ItemDataManager.instance.GetItemData(equipmentSlot[itemSlot]).power;
            partsControl.UnEquippedArmor(itemSlot);
        }
        equipmentSlot[itemSlot] = itemNum;
        equipmentControl.SetSlotImage();
        playerState.curDef = playerState.curDef + ItemDataManager.instance.GetItemData(itemNum).power;
        partsControl.EquippedArmor(itemSlot, armorNum.ToString());
    }

    public int GetSlotItemNum(string slot)
    {
        return equipmentSlot[slot];
    }

    public void SetEquipmentSlotObject(EquipmentUI control)
    {
        equipmentControl = control;
    }

    public void Non()
    {

    }
}
