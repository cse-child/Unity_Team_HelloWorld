using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerPartsControl : MonoBehaviour
{
    public GameObject armorSocket;
    public GameObject weaponSocket;

    private PlayerControl playerControl;

    private Dictionary<string, string> useIndices = new Dictionary<string, string>()
    {
        {"hat", "0" },
        {"top", "0" },
        {"pants", "0" },
        {"shoes", "0" },
        {"weapon", "1" }
    };

    [SerializeField] private string hat, top, pants, shoes, weapon;

    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    // Test Function
    private void Update()
    {
        // Inspector Ȯ�ο� ���� �ʱ�ȭ
        UpdateInspector();

        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    UnEquippedArmor("hat");
        //    UnEquippedArmor("top");
        //    UnEquippedArmor("pants");
        //    UnEquippedArmor("shoes");
        //    UnEquippedWeapon();
        //}
        //else if(Input.GetKeyDown(KeyCode.F2))
        //{
        //    EquippedArmor("hat", "1");
        //    EquippedArmor("top", "1");
        //    EquippedArmor("pants", "1");
        //    EquippedArmor("shoes", "1");
        //    EquippedWeapon("1");
        //}
        //else if (Input.GetKeyDown(KeyCode.F3))
        //{
        //    EquippedArmor("hat", "2");
        //    EquippedArmor("top", "2");
        //    EquippedArmor("pants", "2");
        //    EquippedArmor("shoes", "2");
        //    EquippedWeapon("2");
        //}
        //else if (Input.GetKeyDown(KeyCode.F4))
        //{
        //    EquippedArmor("hat", "3");
        //    EquippedArmor("top", "3");
        //    EquippedArmor("pants", "3");
        //    EquippedArmor("shoes", "3");
        //    EquippedWeapon("3");
        //}
    }

    private void UpdateInspector()
    {
        hat = useIndices["hat"];
        top = useIndices["top"];
        pants = useIndices["pants"];
        shoes = useIndices["shoes"];
        weapon = useIndices["weapon"];
    }

    /* �� ���� */
    public void EquippedArmor(string part, string index)
    {
        // �������� Part ��Ȱ��ȭ
        armorSocket.transform.Find(part).Find(useIndices[part]).gameObject.SetActive(false);
        
        // �����Ϸ��� Part Ȱ��ȭ
        armorSocket.transform.Find(part).Find(index).gameObject.SetActive(true);
        useIndices[part] = index;
    }

    /* �� �������� */
    public void UnEquippedArmor(string part)
    {
        // �������� Part ��Ȱ��ȭ
        armorSocket.transform.Find(part).Find(useIndices[part]).gameObject.SetActive(false);

        // ���� ������ �𵨸� ����
        armorSocket.transform.Find(part).Find("0").gameObject.SetActive(true);
        useIndices[part] = "0";
    }

    /* ���� ���� */
    public void EquippedWeapon(string index)
    {
        // �������� ���� ��Ȱ��ȭ
        weaponSocket.transform.Find(useIndices["weapon"]).gameObject.SetActive(false);

        // �����Ϸ��� ���� Ȱ��ȭ
        weaponSocket.transform.Find(index).gameObject.SetActive(true);
        useIndices["weapon"] = index;
    }

    /* ���� �������� */
    public void UnEquippedWeapon()
    {
        // �������� ���� ��Ȱ��ȭ
        weaponSocket.transform.Find(useIndices["weapon"]).gameObject.SetActive(false);

        // ���� ���� �ε��� ����
        weaponSocket.transform.Find("0").gameObject.SetActive(true);
        useIndices["weapon"] = "0";
        playerControl.SetWeaponState(0);
    }
}
